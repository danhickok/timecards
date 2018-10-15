using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;

namespace TimecardsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Configuration.Load();

            var mainForm = new frmMain();

            mainForm.InitialPositioning = true;

            if (Configuration.MainFormHeight > 0)
                mainForm.Height = Configuration.MainFormHeight;
            if (Configuration.MainFormWidth > 0)
                mainForm.Width = Configuration.MainFormWidth;

            if (Configuration.MainFormTop < 0 && Configuration.MainFormLeft < 0)
            {
                mainForm.Top = Screen.PrimaryScreen.WorkingArea.Height - mainForm.Height / 2;
                mainForm.Left = Screen.PrimaryScreen.WorkingArea.Width - mainForm.Width / 2;
            }
            else
            {
                mainForm.Top = Configuration.MainFormTop;
                mainForm.Left = Configuration.MainFormLeft;
            }

            mainForm.InitialPositioning = false;

            Application.Run(mainForm);
        }
    }
}
