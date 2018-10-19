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

namespace TimecardsUI
{
    public partial class frmMain : Form
    {
        public bool InitialPositioning = false;

        public frmMain()
        {
            InitializeComponent();

            grdActivities.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            grdActivities.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ButtonFace;
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

        private void grdActivities_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            RecalculateColumnWidths(e.Column);
        }

        private void RecalculateColumnWidths(DataGridViewColumn targetColumn = null)
        {
            var oldTotalWidth = grdActivities.RowHeadersWidth
                + colCode.Width + colDescription.Width + colTime.Width;

            var newTotalWidth = grdActivities.ClientRectangle.Width;
            var delta = newTotalWidth - oldTotalWidth - 2;

            //if (targetColumn != null)
            //    targetColumn.Width += delta;
            //else
            colDescription.Width += delta;
        }
    }
}
