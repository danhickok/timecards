﻿using System;
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
    public partial class frmMain : Form
    {
        public bool InitialPositioning = false;
        public IFactory Factory = null;

        private bool _loading = false;
        private Keys _lastGridKeyCode = 0;
        private VScrollBar _gridVScrollBar = null;

        public frmMain()
        {
            _loading = true;

            InitializeComponent();

            colCode.Width = MainFormSettings.ColumnCodeWidth;
            colTime.Width = MainFormSettings.ColumnTimeWidth;

            _loading = false;

            grdActivities.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            grdActivities.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ButtonFace;
            FindGridVScrollBarControl();

            ClearStatusMessage();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            RecalculateColumnWidths();
        }

        private void frmMain_Move(object sender, EventArgs e)
        {
            if (InitialPositioning)
                return;

            MainFormSettings.Top = Top;
            MainFormSettings.Left = Left;
            MainFormSettings.Height = Height;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (InitialPositioning)
                return;

            if (WindowState == FormWindowState.Minimized)
                return;

            MainFormSettings.Height = Height;
            MainFormSettings.Width = Width;

            RecalculateColumnWidths();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _gridVScrollBar = null;
        }

        private void mnuFileMainExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuMainFilePreferences_Click(object sender, EventArgs e)
        {
            var configForm = new frmConfiguration();
            configForm.ShowDialog(this);
        }

        private void mnuMainFileResetColumnWidths_Click(object sender, EventArgs e)
        {
            colCode.Width = 80;
            colTime.Width = 80;
        }

        private void mnuMainFileExport_Click(object sender, EventArgs e)
        {
            var exportForm = new frmExport();
            exportForm.ShowDialog(this);
        }

        private void mnuMainFileImport_Click(object sender, EventArgs e)
        {
            var importForm = new frmImport();
            importForm.ShowDialog(this);
        }

        private void mnuMainHelpAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new frmAbout();
            aboutForm.ShowDialog(this);
        }

        private void mnuMainDataDateFirst_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void mnuMainDataDatePrevious_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void mnuMainDataDateNext_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void mnuMainDataDateLast_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void mnuDataSearchForDate_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void mnuMainDataActivitiesSort_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var dateSearchForm = new frmDateSearch();
            dateSearchForm.ShowDialog();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            lblDayOfWeek.Text = dtpDate.Value.DayOfWeek.ToString();
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

            MainFormSettings.ColumnCodeWidth = colCode.Width;
            MainFormSettings.ColumnTimeWidth = colTime.Width;
        }

        private void grdActivities_KeyDown(object sender, KeyEventArgs e)
        {
            _lastGridKeyCode = e.KeyCode;
        }

        private void grdActivities_Leave(object sender, EventArgs e)
        {
            if (_lastGridKeyCode == Keys.Tab)
                grdActivities.Focus();
        }

        private void SetStatusMessage(string message)
        {
            staMainLabel.Text = message;
            Refresh();
        }

        private void ClearStatusMessage()
        {
            staMainLabel.Text = "Ready";
            Refresh();
        }

        private void FindGridVScrollBarControl()
        {
            _gridVScrollBar = null;
            for (int i = 0; i < grdActivities.Controls.Count; ++i)
            {
                if (grdActivities.Controls[i] is VScrollBar)
                {
                    _gridVScrollBar = grdActivities.Controls[i] as VScrollBar;
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

            var availableWidth = grdActivities.ClientRectangle.Width;
            if (_gridVScrollBar != null && _gridVScrollBar.Visible)
                availableWidth -= _gridVScrollBar.Width;

            if (eventColumn?.Name == "colDescription")
                colTime.Width = availableWidth
                    - colCode.Width - colDescription.Width - grdActivities.RowHeadersWidth - 2;
            else
                colDescription.Width = availableWidth
                    - colCode.Width - colTime.Width - grdActivities.RowHeadersWidth - 2;

            _loading = false;
        }
    }
}
