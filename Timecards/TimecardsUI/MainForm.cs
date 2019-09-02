using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;
using TimecardsCore.Interfaces;

namespace TimecardsUI
{
    public partial class MainForm : Form
    {
        public bool InitialPositioning = false;
        public IFactory Factory = null;

        private bool _loading = false;
        private Keys _lastGridKeyCode = 0;
        private VScrollBar _gridVScrollBar = null;

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
            if (_gridVScrollBar != null)
                _gridVScrollBar.Dispose();
            _gridVScrollBar = null;

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
        }

        private void MainMenuFileResetColumnWidths_Click(object sender, EventArgs e)
        {
            CodeColumn.Width = 80;
            TimeColumn.Width = 80;
        }

        private void MainMenuFileExport_Click(object sender, EventArgs e)
        {
            var exportForm = new frmExport();
            exportForm.ShowDialog(this);
        }

        private void MainMenuFileImport_Click(object sender, EventArgs e)
        {
            var importForm = new frmImport();
            importForm.ShowDialog(this);
        }

        private void MainMenuHelpAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
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
            var dateSearchForm = new frmDateSearch();
            dateSearchForm.ShowDialog();
        }

        private void ReportButtonGo_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            MainDateLabel.Text = MainDate.Value.DayOfWeek.ToString();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            //TODO:
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            //TODO:
        }

        private void grdActivities_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            RecalculateColumnWidths();
        }

        private void grdActivities_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_loading)
                return;

            RecalculateColumnWidths(e.Column);

            MainFormSettings.ColumnCodeWidth = CodeColumn.Width;
            MainFormSettings.ColumnTimeWidth = TimeColumn.Width;
        }

        private void grdActivities_KeyDown(object sender, KeyEventArgs e)
        {
            _lastGridKeyCode = e.KeyCode;
        }

        private void grdActivities_Leave(object sender, EventArgs e)
        {
            if (_lastGridKeyCode == Keys.Tab)
                ActivitiesGrid.Focus();
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
            _gridVScrollBar = null;
            for (int i = 0; i < ActivitiesGrid.Controls.Count; ++i)
            {
                if (ActivitiesGrid.Controls[i] is VScrollBar)
                {
                    _gridVScrollBar = ActivitiesGrid.Controls[i] as VScrollBar;
                    break;
                }
            }

            if (_gridVScrollBar != null)
            {
                _gridVScrollBar.VisibleChanged += new EventHandler(grdVScrollBar_VisibleChanged);
            }
        }

        private void grdVScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            RecalculateColumnWidths();
        }

        private void RecalculateColumnWidths(DataGridViewColumn eventColumn = null)
        {
            _loading = true;

            var availableWidth = ActivitiesGrid.ClientRectangle.Width;
            if (_gridVScrollBar != null && _gridVScrollBar.Visible)
                availableWidth -= _gridVScrollBar.Width;

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
