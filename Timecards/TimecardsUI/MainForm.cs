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
        }

        private void mnuFileMainExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

            MainFormSettings.Top = this.Top;
            MainFormSettings.Left = this.Left;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (InitialPositioning)
                return;

            if (this.WindowState == FormWindowState.Minimized)
                return;

            MainFormSettings.Height = this.Height;
            MainFormSettings.Width = this.Width;
        }
    }
}
