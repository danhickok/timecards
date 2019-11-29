using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimecardsCore;
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

        // this object is disposed of in FormClosed event
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality",
            "IDE0069:Disposable fields should be disposed", Justification = "<Pending>")]
        private VScrollBar _activitiesGridVScrollBar = null;

        private Color _beforeMidnightBackgroundColor;
        private Color _beforeMidnightAlternateBackgroundColor;
        private Color _afterMidnightBackgroundColor;
        private Color _afterMidnightAlternateBackgroundColor;

        private TimecardLogic _timecardLogic = null;
        private ReportLogic _reportLogic = null;

        public MainForm()
        {
            _loading = true;

            InitializeComponent();

            CodeColumn.Width = MainFormSettings.ColumnCodeWidth;
            TimeColumn.Width = MainFormSettings.ColumnTimeWidth;

            _loading = false;

            _beforeMidnightBackgroundColor = SystemColors.Window;
            _beforeMidnightAlternateBackgroundColor = SystemColors.ButtonFace;
            SetAfterMidnightRowColors();
            
            // fix the creeping bottom edge design problem in VS
            ActivitiesGrid.Height =
                MainTabActivities.ClientRectangle.Height - ActivitiesGrid.Top - ActivitiesGrid.Left;
            
            FindGridVScrollBarControl();

            ClearStatusMessage();
        }

        // allows form to be closed when control validation would otherwise prevent it
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;
            base.OnFormClosing(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log("MainForm_Load event");

            _timecardLogic = new TimecardLogic(Factory);
            _reportLogic = new ReportLogic(Factory);

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
            Log("MainForm_Activated event");

            RecalculateColumnWidths();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            Log("MainForm_Move event");

            if (InitialPositioning)
                return;

            MainFormSettings.Top = Top;
            MainFormSettings.Left = Left;
            MainFormSettings.Height = Height;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Log("MainForm_Resize event");

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
            Log("MainForm_FormClosed event");

            if (_activitiesGridVScrollBar != null)
                _activitiesGridVScrollBar.Dispose();
            _activitiesGridVScrollBar = null;

            if (_timecardLogic != null && _timecardLogic.GetCurrentTimecard().IsDirty)
                _timecardLogic.SaveTimecard();

            _timecardLogic = null;
            _reportLogic = null;

            if (Factory != null)
                Factory.Dispose();
        }

        private void MainMenuFileExit_Click(object sender, EventArgs e)
        {
            Log("MainMenuFileExit_Click event");

            Close();
        }

        private void MainMenuFilePreferences_Click(object sender, EventArgs e)
        {
            Log("MainMenuFilePreferences_Click event");

            var configForm = new ConfigurationForm();
            configForm.ShowDialog(this);

            if (configForm.ConfigurationChanged)
            {
                SetAfterMidnightRowColors();
                PopulateActivitiesGrid();
            }

            configForm.Dispose();
        }

        private void MainMenuFileResetColumnWidths_Click(object sender, EventArgs e)
        {
            Log("MainMenuFileResetColumnWidths_Click event");

            CodeColumn.Width = 80;
            TimeColumn.Width = 80;
        }

        private void MainMenuFileExport_Click(object sender, EventArgs e)
        {
            Log("MainMenuFileExport_Click event");

            var exportForm = new ExportForm();
            exportForm.ShowDialog(this);
            exportForm.Dispose();
        }

        private void MainMenuFileImport_Click(object sender, EventArgs e)
        {
            Log("MainMenuFileImport_Click event");

            var importForm = new ImportForm();
            importForm.ShowDialog(this);
            importForm.Dispose();
        }

        private void MainMenuHelpAbout_Click(object sender, EventArgs e)
        {
            Log("MainMenuHelpAbout_Click event");

            var aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
            aboutForm.Dispose();
        }

        private void MainMenuDataDateFirst_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataDateFirst_Click event");

            NavigateTo(Navigation.Earliest);
        }

        private void MainMenuDataDatePrevious_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataDatePrevious_Click event");

            NavigateTo(Navigation.Previous);
        }

        private void MainMenuDataDateNext_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataDateNext_Click event");

            NavigateTo(Navigation.Next);
        }

        private void MainMenuDataDateLast_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataDateLast_Click event");

            NavigateTo(Navigation.Latest);
        }

        private void MainMenuDataDeleteTimecard_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataDeleteTimecard_Click event");

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
            Log("MainMenuDataSearchForDate_Click event");

            SelectTimecardBySearchForm();
        }

        private void MainMenuDataActivitiesSort_Click(object sender, EventArgs e)
        {
            Log("MainMenuDataActivitiesSort_Click event");

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
            Log("MainMenuDataToggleAfterMidnight_Click event");

            MainMenuDataToggleAfterMidnight.Checked = !MainMenuDataToggleAfterMidnight.Checked;
        }

        private void MainMenuDataToggleAfterMidnight_CheckStateChanged(object sender, EventArgs e)
        {
            Log("MainMenuDataToggleAfterMidnight_CheckStateChanged event");

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
            Log("NavButtonFirst_Click event");

            NavigateTo(Navigation.Earliest);
            ActivitiesGrid.Focus();
        }

        private void NavButtonPrev_Click(object sender, EventArgs e)
        {
            Log("NavButtonPrev_Click event");

            NavigateTo(Navigation.Previous);
            ActivitiesGrid.Focus();
        }

        private void NavButtonNext_Click(object sender, EventArgs e)
        {
            Log("NavButtonNext_Click event");

            NavigateTo(Navigation.Next);
            ActivitiesGrid.Focus();
        }

        private void NavButtonLast_Click(object sender, EventArgs e)
        {
            Log("NavButtonLast_Click event");

            NavigateTo(Navigation.Latest);
            ActivitiesGrid.Focus();
        }

        private void NavButtonToday_Click(object sender, EventArgs e)
        {
            Log("NavButtonToday_Click event");

            NavigateTo(Navigation.Today);
            ActivitiesGrid.Focus();
        }

        private void NavButtonSearch_Click(object sender, EventArgs e)
        {
            Log("NavButtonSearch_Click event");

            SelectTimecardBySearchForm();
        }

        private void SelectTimecardBySearchForm()
        {
            Log("SelectTimecardBySearchForm");

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
            Log($"NavigateTo: direction={direction}");

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
            Log("ReportButtonGo_Click event");

            SetStatusMessage("Gathering data...");

            var report = _reportLogic.GetReport(
                ReportDateStart.Value.Date, ReportDateEnd.Value.Date);
            
            ReportListView.Items.Clear();

            var totalMinutes = 0;
            var totalHours = 0M;

            foreach (var item in report)
            {
                var hours = Math.Round(item.TotalMinutes / 60M, 2);

                var row = new ListViewItem(item.Code);
                row.SubItems.AddRange(new[]
                {
                    $"{item.EarliestDate:d}",
                    $"{item.LatestDate:d}",
                    $"{item.TotalMinutes:N0}",
                    $"{hours:N2}",
                });

                ReportListView.Items.Add(row);
                
                totalMinutes += item.TotalMinutes;
                totalHours += hours;
            }

            ReportListView.Items.Add(string.Empty);

            var totals = new ListViewItem();
            totals.SubItems.AddRange(new[]
            {
                string.Empty,
                "Total",
                $"{totalMinutes:N0}",
                $"{totalHours:N2}",
            });
            ReportListView.Items.Add(totals);

            ClearStatusMessage();
        }

        private void MainDate_ValueChanged(object sender, EventArgs e)
        {
            Log("MainDate_ValueChanged event");

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
            Log("ReportDateStart_ValueChanged event");

            if (_loading)
                return;

            //TODO:
        }

        private void ReportDateEnd_ValueChanged(object sender, EventArgs e)
        {
            Log("ReportDateEnd_ValueChanged event");

            if (_loading)
                return;

            //TODO:
        }

        private void ActivitiesGrid_ClientSizeChanged(object sender, EventArgs e)
        {
            Log("ActivitiesGrid_ClientSizeChanged event");

            if (_loading)
                return;

            RecalculateColumnWidths();
        }

        private void ActivitiesGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Log("ActivitiesGrid_ColumnWidthChanged event");

            if (_loading)
                return;

            RecalculateColumnWidths(e.Column);

            MainFormSettings.ColumnCodeWidth = CodeColumn.Width;
            MainFormSettings.ColumnTimeWidth = TimeColumn.Width;
        }

        // allows delete key to erase current cell if highlighted (not in edit mode)
        private void ActivitiesGrid_KeyDown(object sender, KeyEventArgs e)
        {
            Log("ActivitiesGrid_KeyDown event");

            if (e.KeyCode == Keys.Delete)
            {
                if (!ActivitiesGrid.CurrentCell.IsInEditMode)
                {
                    ActivitiesGrid.CurrentCell.Value = string.Empty;
                }
            }
        }

        private void ActivitiesGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Log($"ActivitiesGrid_CellEnter event: RowIndex={e.RowIndex}, ColumnIndex={e.ColumnIndex}");

            if (e.ColumnIndex == 1 &&
                string.IsNullOrWhiteSpace(ActivitiesGrid.CurrentCell.Value?.ToString()))
            {
                var code = ActivitiesGrid.CurrentRow.Cells[0].Value.ToString();
                if (Configuration.DefaultCodes.ContainsKey(code))
                {
                    ActivitiesGrid.CurrentCell.Value = Configuration.DefaultCodes[code];
                }
            }
        }

        private void ActivitiesGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Log($"ActivitiesGrid_CellBeginEdit event: RowIndex={e.RowIndex}, ColumnIndex={e.ColumnIndex}");

            if (_loading)
                return;

            if (_timecardLogic == null)
                return;

            _loading = true;

            var activity = CurrentActivity(e.RowIndex);
            
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1)
                && string.IsNullOrWhiteSpace(ActivitiesGrid.Rows[e.RowIndex].Cells[2].Value?.ToString()))
            {
                ActivitiesGrid.Rows[e.RowIndex].Cells[2].Value = activity.Time;
            }

            _loading = false;
        }

        private void ActivitiesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Log($"ActivitiesGrid_CellValueChanged event: RowIndex={e.RowIndex}, ColumnIndex={e.ColumnIndex}");

            if (_loading)
                return;

            if (_timecardLogic == null)
                return;

            _loading = true;

            var activity = CurrentActivity(e.RowIndex);

            var value = ActivitiesGrid.Rows[e.RowIndex].Cells[e.ColumnIndex]?.Value?.ToString() ?? string.Empty;

            switch (e.ColumnIndex)
            {
                case 0:
                    Log($"Setting row {e.RowIndex} Code = \"{value}\"");
                    activity.Code = value;
                    break;
                case 1:
                    Log($"Setting row {e.RowIndex} Description = \"{value}\"");
                    activity.Description = value;
                    break;
                case 2:
                    Log($"Setting row {e.RowIndex} Time = \"{value}\"");
                    activity.Time = value;
                    Log($"Setting row {e.RowIndex} Time reformatted to \"{activity.Time}\"");
                    ActivitiesGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = activity.Time;
                    break;
            }

            Log("Saving timecard");
            _timecardLogic.SaveTimecard();

            _loading = false;
        }

        private void ActivitiesGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Log($"ActivitiesGrid_UserDeletingRow event: Row.Index={e.Row.Index}");

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
            //Log($"ActivitiesGrid_RowPrePaint event: RowIndex={e.RowIndex}");

            var index = e.RowIndex;
            if ((bool?)ActivitiesGrid.Rows[index].Tag ?? false)
                ActivitiesGrid.Rows[index].DefaultCellStyle.BackColor =
                    index % 2 == 0
                        ? _afterMidnightBackgroundColor
                        : _afterMidnightAlternateBackgroundColor;
        }

        private void ActivitiesGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Log($"ActivitiesGrid_RowEnter event: RowIndex={e.RowIndex}, ColumnIndex={e.ColumnIndex}");

            if (_loading)
                return;

            if (_timecardLogic == null)
                return;

            _loading = true;

            if (TimecardHasRow(e.RowIndex))
            {
                var activity = CurrentActivity(e.RowIndex);
                MainMenuDataToggleAfterMidnight.Checked = activity.IsAfterMidnight;
            }

            _loading = false;
        }

        private void PopulateActivitiesGrid()
        {
            Log("PopulateActivitiesGrid");

            _loading = true;

            var tc = _timecardLogic.GetCurrentTimecard();

            ActivitiesGrid.Rows.Clear();
            foreach (var activity in tc.Activities)
            {
                var index = ActivitiesGrid.Rows.Add(activity.Code, activity.Description, activity.Time);
                ActivitiesGrid.Rows[index].Tag = activity.IsAfterMidnight;
            }

            _loading = false;
        }

        private bool TimecardHasRow(int row)
        {
            Log($"TimecardHasRow: row={row}");

            var tc = _timecardLogic.GetCurrentTimecard();
            return row <= tc.Activities.Count - 1;
        }

        private Activity CurrentActivity(int row)
        {
            Log($"CurrentActivity: row={row}");

            var tc = _timecardLogic.GetCurrentTimecard();

            while (tc.Activities.Count() - 1 < row)
            {
                Log($"Adding new Activity, asking for row {row}");
                tc.Activities.Add(new Activity());
            }

            return tc.Activities[row];
        }

        private void SetRowBackgroundColor(int index, bool isAfterMidnight)
        {
            Log($"SetRowBackgroundColor: index={index}, isAfterMidnight={isAfterMidnight}");

            _loading = true;

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

            _loading = false;
        }

        private void SetStatusMessage(string message)
        {
            Log("SetStatusMessage");

            MainStatusLabel.Text = message;
            Refresh();
        }

        private void ClearStatusMessage()
        {
            Log("ClearStatusMessage");

            MainStatusLabel.Text = string.Empty;
            Refresh();
        }

        private void SetAfterMidnightRowColors()
        {
            Log("SetAfterMidnightRowColors");

            var tint = Configuration.MidnightTint;
            double factorR = tint.R / 255.0;
            double factorG = tint.G / 255.0;
            double factorB = tint.B / 255.0;

            _afterMidnightBackgroundColor =
                Color.FromArgb(
                    (byte)Math.Floor(SystemColors.Window.R * factorR),
                    (byte)Math.Floor(SystemColors.Window.G * factorG),
                    (byte)Math.Floor(SystemColors.Window.B * factorB));
            _afterMidnightAlternateBackgroundColor =
                Color.FromArgb(
                    (byte)Math.Floor(SystemColors.ButtonFace.R * factorR),
                    (byte)Math.Floor(SystemColors.ButtonFace.G * factorG),
                    (byte)Math.Floor(SystemColors.ButtonFace.B * factorB));

            ActivitiesGrid.RowsDefaultCellStyle.BackColor = _beforeMidnightBackgroundColor;
            ActivitiesGrid.AlternatingRowsDefaultCellStyle.BackColor = _beforeMidnightAlternateBackgroundColor;
        }

        private void FindGridVScrollBarControl()
        {
            Log("FindGridVScrollBarControl");

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
            Log("ActivitiesGridVScrollBar_VisibleChanged event");

            RecalculateColumnWidths();
        }

        private void RecalculateColumnWidths(DataGridViewColumn eventColumn = null)
        {
            Log($"RecalculateColumnWidths: eventColumn.Index={eventColumn?.Index}");

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

        private void Log(string message)
        {
            Debug.Print($"{DateTime.Now:HH:mm:ss.fff}  {message}");
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
