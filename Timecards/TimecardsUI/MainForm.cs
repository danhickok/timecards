﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore.Models;
using TimecardsCore.Logic;
using TimecardsCore.Interfaces;
using TimecardsCore.Exceptions;

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
        
        private TimecardLogic _timecardLogic = null;

        public MainForm()
        {
            _loading = true;

            InitializeComponent();

            CodeColumn.Width = MainFormSettings.ColumnCodeWidth;
            TimeColumn.Width = MainFormSettings.ColumnTimeWidth;

            _loading = false;

            ActivitiesGrid.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            ActivitiesGrid.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ButtonFace;
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
            SetStatusMessage("Ready");

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
            //TODO:
        }

        private void MainMenuDataDatePrevious_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void MainMenuDataDateNext_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void MainMenuDataDateLast_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void MainMenuDataSearchForDate_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void MainMenuDataActivitiesSort_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonFirst_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonPrev_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonNext_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonLast_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonToday_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void NavButtonSearch_Click(object sender, EventArgs e)
        {
            var dateSearchForm = new DateSearchForm();
            dateSearchForm.ShowDialog();
            dateSearchForm.Dispose();
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

        private void ActivitiesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
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
            if (MessageBox.Show(this, "Delete this row?", this.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var index = ActivitiesGrid.Rows.IndexOf(e.Row);
                _timecardLogic.DeleteActivity(index);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void PopulateActivitiesGrid()
        {
            ActivitiesGrid.Rows.Clear();
            foreach (var activity in _timecardLogic.GetCurrentTimecard().Activities)
            {
                ActivitiesGrid.Rows.Add(activity.Code, activity.Description, activity.Time);
            }
        }

        private void SetStatusMessage(string message)
        {
            MainStatusLabel.Text = message;
            Refresh();
        }

        private void ClearStatusMessage()
        {
            MainStatusLabel.Text = "Ready";
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
    }
}
