using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimecardsCore.Exceptions;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;
using TimecardsCore.Models;

namespace TimecardsUI
{
    public partial class MainForm : Form
    {
        public bool InitialPositioning = false;
        public IFactory Factory = null;

        private bool _loading = false;
        private Keys _lastGridKeyCode = 0;

        // this object is disposed of in FormClosed event
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality",
            "IDE0069:Disposable fields should be disposed", Justification = "<Pending>")]
        private VScrollBar _activitiesGridVScrollBar = null;

        private readonly Color _beforeMidnightBackgroundColor;
        private readonly Color _beforeMidnightAlternateBackgroundColor;
        private readonly Color _afterMidnightBackgroundColor;
        private readonly Color _afterMidnightAlternateBackgroundColor;
        
        //TODO: make "after midnight" tint user-configurable
        private readonly float _midnightColorFactorR = 0.89f;
        private readonly float _midnightColorFactorG = 0.98f;
        private readonly float _midnightColorFactorB = 1.00f;

        private TimecardLogic _timecardLogic = null;

        public MainForm()
        {
            _loading = true;

            InitializeComponent();

            CodeColumn.Width = MainFormSettings.ColumnCodeWidth;
            TimeColumn.Width = MainFormSettings.ColumnTimeWidth;

            _loading = false;

            // set colors
            _beforeMidnightBackgroundColor = SystemColors.Window;
            _beforeMidnightAlternateBackgroundColor = SystemColors.ButtonFace;

            _afterMidnightBackgroundColor =
                Color.FromArgb(
                    (byte)Math.Floor(SystemColors.Window.R * _midnightColorFactorR),
                    (byte)Math.Floor(SystemColors.Window.G * _midnightColorFactorG),
                    (byte)Math.Floor(SystemColors.Window.B * _midnightColorFactorB));
            _afterMidnightAlternateBackgroundColor =
                Color.FromArgb(
                    (byte)Math.Floor(SystemColors.ButtonFace.R * _midnightColorFactorR),
                    (byte)Math.Floor(SystemColors.ButtonFace.G * _midnightColorFactorG),
                    (byte)Math.Floor(SystemColors.ButtonFace.B * _midnightColorFactorB));

            ActivitiesGrid.RowsDefaultCellStyle.BackColor = _beforeMidnightBackgroundColor;
            ActivitiesGrid.AlternatingRowsDefaultCellStyle.BackColor = _beforeMidnightAlternateBackgroundColor;
            
            // fix the creeping bottom edge design problem in VS
            ActivitiesGrid.Height =
                MainTabActivities.ClientRectangle.Height - ActivitiesGrid.Top - ActivitiesGrid.Left;
            
            FindGridVScrollBarControl();

            ClearStatusMessage();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _timecardLogic = new TimecardLogic(Factory);

            _loading = true;

            Timecard tc;
            try
            {
                tc = _timecardLogic.GetLatestTimecard();
            }
            catch (TimecardNotFoundException)
            {
                tc = _timecardLogic.GetNewTimecard();
            }
            
            MainDate.Value = tc.Date;
            UpdateMainDateLabel();
            PopulateActivitiesGrid();
            ClearStatusMessage();

            _loading = false;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            RecalculateColumnWidths();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            if (InitialPositioning)
                return;

            MainFormSettings.Top = Top;
            MainFormSettings.Left = Left;
            MainFormSettings.Height = Height;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (InitialPositioning)
                return;

            if (WindowState == FormWindowState.Minimized)
                return;

            MainFormSettings.Height = Height;
            MainFormSettings.Width = Width;

            RecalculateColumnWidths();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_activitiesGridVScrollBar != null)
                _activitiesGridVScrollBar.Dispose();
            _activitiesGridVScrollBar = null;

            if (_timecardLogic != null && _timecardLogic.GetCurrentTimecard().IsDirty)
                _timecardLogic.SaveTimecard();

            _timecardLogic = null;

            if (Factory != null)
                Factory.Dispose();
        }

        private void MainMenuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainMenuFilePreferences_Click(object sender, EventArgs e)
        {
            var configForm = new ConfigurationForm();
            configForm.ShowDialog(this);
            configForm.Dispose();
        }

        private void MainMenuFileResetColumnWidths_Click(object sender, EventArgs e)
        {
            CodeColumn.Width = 80;
            TimeColumn.Width = 80;
        }

        private void MainMenuFileExport_Click(object sender, EventArgs e)
        {
            var exportForm = new ExportForm();
            exportForm.ShowDialog(this);
            exportForm.Dispose();
        }

        private void MainMenuFileImport_Click(object sender, EventArgs e)
        {
            var importForm = new ImportForm();
            importForm.ShowDialog(this);
            importForm.Dispose();
        }

        private void MainMenuHelpAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
            aboutForm.Dispose();
        }

        private void MainMenuDataDateFirst_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Earliest);
        }

        private void MainMenuDataDatePrevious_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Previous);
        }

        private void MainMenuDataDateNext_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Next);
        }

        private void MainMenuDataDateLast_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Latest);
        }

        private void MainMenuDataDeleteTimecard_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,
                "Delete this timecard?\nWarning: all activity for this timecard will also be deleted",
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SetStatusMessage("Deleting timecard...");

                try
                {
                    _timecardLogic.DeleteTimecard();
                }
                catch (TimecardNotFoundException)
                {
                    _timecardLogic.GetNewTimecard();
                }
                
                MainDate.Value = _timecardLogic.GetCurrentTimecard().Date;
                UpdateMainDateLabel();
                PopulateActivitiesGrid();

                ClearStatusMessage();
            }
        }

        private void MainMenuDataSearchForDate_Click(object sender, EventArgs e)
        {
            SelectTimecardBySearchForm();
        }

        private void MainMenuDataActivitiesSort_Click(object sender, EventArgs e)
        {
            SetStatusMessage("Sorting activities by time...");

            var tc = _timecardLogic.GetCurrentTimecard();
            tc.Activities.Sort((a, b) => {
                if (a.StartMinute < b.StartMinute)
                    return -1;
                else if (a.StartMinute > b.StartMinute)
                    return 1;
                else
                    return 0;
            });
            PopulateActivitiesGrid();

            ClearStatusMessage();
        }

        private void MainMenuDataToggleAfterMidnight_Click(object sender, EventArgs e)
        {
            MainMenuDataToggleAfterMidnight.Checked = !MainMenuDataToggleAfterMidnight.Checked;
        }

        private void MainMenuDataToggleAfterMidnight_CheckStateChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            var tc = _timecardLogic.GetCurrentTimecard();

            var index = ActivitiesGrid.CurrentRow.Index;
            if (index < 0 || index > tc.Activities.Count - 1)
            {
                _loading = true;
                MainMenuDataToggleAfterMidnight.Checked = false;
                _loading = false;

                return;
            }

            tc.Activities[index].IsAfterMidnight = MainMenuDataToggleAfterMidnight.Checked;
            ActivitiesGrid.Rows[index].Tag = MainMenuDataToggleAfterMidnight.Checked;
            SetRowBackgroundColor(index, MainMenuDataToggleAfterMidnight.Checked);
            _timecardLogic.SaveTimecard();
        }

        private void NavButtonFirst_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Earliest);
            ActivitiesGrid.Focus();
        }

        private void NavButtonPrev_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Previous);
            ActivitiesGrid.Focus();
        }

        private void NavButtonNext_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Next);
            ActivitiesGrid.Focus();
        }

        private void NavButtonLast_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Latest);
            ActivitiesGrid.Focus();
        }

        private void NavButtonToday_Click(object sender, EventArgs e)
        {
            NavigateTo(Navigation.Today);
            ActivitiesGrid.Focus();
        }

        private void NavButtonSearch_Click(object sender, EventArgs e)
        {
            SelectTimecardBySearchForm();
        }

        private void SelectTimecardBySearchForm()
        {
            SetStatusMessage("Loading timecard list...");
            var tcList = _timecardLogic.GetTimecardList();
            var dateSearchForm = new DateSearchForm
            {
                TimecardList = tcList
            };
            ClearStatusMessage();

            dateSearchForm.ShowDialog();
            var selectedID = dateSearchForm.SelectedTimecardID;
            var canceled = dateSearchForm.Canceled;
            dateSearchForm.Dispose();

            if (!canceled && selectedID != 0)
            {
                try
                {
                    SetStatusMessage("Loading timecard...");

                    _timecardLogic.GetSpecificTimecard(selectedID);

                    _loading = true;
                    MainDate.Value = _timecardLogic.GetCurrentTimecard().Date;
                    UpdateMainDateLabel();
                    PopulateActivitiesGrid();
                    _loading = false;
                }
                catch (TimecardNotFoundException)
                {
                    MessageBox.Show("Timecard not found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearStatusMessage();
                }
            }
        }

        private void NavigateTo(Navigation direction)
        {
            _loading = true;
            SetStatusMessage("Loading...");

            try
            {
                switch (direction)
                {
                    case Navigation.Earliest:
                        _timecardLogic.GetEarliestTimecard();
                        break;
                    case Navigation.Previous:
                        _timecardLogic.GetPreviousTimecard();
                        break;
                    case Navigation.Next:
                        _timecardLogic.GetNextTimecard();
                        break;
                    case Navigation.Latest:
                        _timecardLogic.GetLatestTimecard();
                        break;
                    case Navigation.Today:
                        _timecardLogic.GetTodaysTimecard();
                        break;
                }
            }
            catch (TimecardNotFoundException)
            {
                _timecardLogic.GetNewTimecard();
            }

            MainDate.Value = _timecardLogic.GetCurrentTimecard().Date;
            UpdateMainDateLabel();
            PopulateActivitiesGrid();

            ClearStatusMessage();
            _loading = false;

        }

        private void ReportButtonGo_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void MainDate_ValueChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            _timecardLogic.GetCurrentTimecard().Date = MainDate.Value;
            _timecardLogic.SaveTimecard();

            UpdateMainDateLabel();
        }

        private void UpdateMainDateLabel()
        {
            MainDateLabel.Text = MainDate.Value.DayOfWeek.ToString();
        }

        private void ReportDateStart_ValueChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            //TODO:
        }

        private void ReportDateEnd_ValueChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            //TODO:
        }

        private void ActivitiesGrid_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            RecalculateColumnWidths();
        }

        private void ActivitiesGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_loading)
                return;

            RecalculateColumnWidths(e.Column);

            MainFormSettings.ColumnCodeWidth = CodeColumn.Width;
            MainFormSettings.ColumnTimeWidth = TimeColumn.Width;
        }

        private void ActivitiesGrid_KeyDown(object sender, KeyEventArgs e)
        {
            _lastGridKeyCode = e.KeyCode;
        }

        private void ActivitiesGrid_Leave(object sender, EventArgs e)
        {
            if (_lastGridKeyCode == Keys.Tab)
                ActivitiesGrid.Focus();
        }

        private void ActivitiesGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_loading)
                return;

            if (_timecardLogic == null)
                return;

            var tc = _timecardLogic.GetCurrentTimecard();

            while (tc.Activities.Count() - 1 < e.RowIndex)
                tc.Activities.Add(new Activity());
            
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1)
                && string.IsNullOrWhiteSpace(ActivitiesGrid.Rows[e.RowIndex].Cells[2].Value?.ToString()))
            {
                _loading = true;
                ActivitiesGrid.Rows[e.RowIndex].Cells[2].Value = tc.Activities[e.RowIndex].Time;
                _loading = false;
            }
        }

            private void ActivitiesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading)
                return;

            if (_timecardLogic == null)
                return;

            var tc = _timecardLogic.GetCurrentTimecard();
            
            while (tc.Activities.Count() - 1 < e.RowIndex)
                tc.Activities.Add(new Activity());

            var value = ActivitiesGrid.CurrentRow.Cells[e.ColumnIndex]?.Value?.ToString() ?? string.Empty;

            switch (e.ColumnIndex)
            {
                case 0:
                    tc.Activities[e.RowIndex].Code = value;
                    break;
                case 1:
                    tc.Activities[e.RowIndex].Description = value;
                    break;
                case 2:
                    tc.Activities[e.RowIndex].Time = value;
                    break;
            }

            _timecardLogic.SaveTimecard();
        }

        private void ActivitiesGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var index = ActivitiesGrid.Rows.IndexOf(e.Row);

            if (MessageBox.Show(this, "Delete this row?", this.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _timecardLogic.DeleteActivity(index);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ActivitiesGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var index = e.RowIndex;
            if ((bool?)ActivitiesGrid.Rows[index].Tag ?? false)
                ActivitiesGrid.Rows[index].DefaultCellStyle.BackColor =
                    index % 2 == 0
                        ? _afterMidnightBackgroundColor
                        : _afterMidnightAlternateBackgroundColor;
        }

        private void ActivitiesGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var tc = _timecardLogic.GetCurrentTimecard();
            if (index < 0 || index > tc.Activities.Count - 1)
                return;

            _loading = true;
            MainMenuDataToggleAfterMidnight.Checked = tc.Activities[index].IsAfterMidnight;
            _loading = false;
        }

        private void PopulateActivitiesGrid()
        {
            ActivitiesGrid.Rows.Clear();
            foreach (var activity in _timecardLogic.GetCurrentTimecard().Activities)
            {
                var index = ActivitiesGrid.Rows.Add(activity.Code, activity.Description, activity.Time);
                ActivitiesGrid.Rows[index].Tag = activity.IsAfterMidnight;
            }
        }

        private void SetRowBackgroundColor(int index, bool isAfterMidnight)
        {
            SetStatusMessage($"Index = {index}");
            if (isAfterMidnight)
            {
                ActivitiesGrid.Rows[index].DefaultCellStyle.BackColor =
                    index % 2 == 0
                    ? _afterMidnightBackgroundColor
                    : _afterMidnightAlternateBackgroundColor;
            }
            else
            {
                ActivitiesGrid.Rows[index].DefaultCellStyle.BackColor =
                    index % 2 == 0
                    ? _beforeMidnightBackgroundColor
                    : _beforeMidnightAlternateBackgroundColor;
            }
        }

        private void SetStatusMessage(string message)
        {
            MainStatusLabel.Text = message;
            Refresh();
        }

        private void ClearStatusMessage()
        {
            MainStatusLabel.Text = string.Empty;
            Refresh();
        }

        private void FindGridVScrollBarControl()
        {
            _activitiesGridVScrollBar = null;
            for (int i = 0; i < ActivitiesGrid.Controls.Count; ++i)
            {
                if (ActivitiesGrid.Controls[i] is VScrollBar)
                {
                    _activitiesGridVScrollBar = ActivitiesGrid.Controls[i] as VScrollBar;
                    break;
                }
            }

            if (_activitiesGridVScrollBar != null)
            {
                _activitiesGridVScrollBar.VisibleChanged += new EventHandler(ActivitiesGridVScrollBar_VisibleChanged);
            }
        }

        private void ActivitiesGridVScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            RecalculateColumnWidths();
        }

        private void RecalculateColumnWidths(DataGridViewColumn eventColumn = null)
        {
            _loading = true;

            var availableWidth = ActivitiesGrid.ClientRectangle.Width;
            if (_activitiesGridVScrollBar != null && _activitiesGridVScrollBar.Visible)
                availableWidth -= _activitiesGridVScrollBar.Width;

            if (eventColumn?.Name == "DescriptionColumn")
                TimeColumn.Width = availableWidth
                    - CodeColumn.Width - DescriptionColumn.Width - ActivitiesGrid.RowHeadersWidth - 2;
            else
                DescriptionColumn.Width = availableWidth
                    - CodeColumn.Width - TimeColumn.Width - ActivitiesGrid.RowHeadersWidth - 2;

            _loading = false;
        }

        private enum Navigation
        {
            Earliest,
            Previous,
            Next,
            Latest,
            Today
        }
    }
}
