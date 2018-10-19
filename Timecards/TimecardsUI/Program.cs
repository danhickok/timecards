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

            if (MainFormSettings.HaveBeenSet)
            {
                mainForm.InitialPositioning = true;

                mainForm.Top = MainFormSettings.Top;
                mainForm.Left = MainFormSettings.Left;
                mainForm.Height = MainFormSettings.Height;
                mainForm.Width = MainFormSettings.Width;

                mainForm.InitialPositioning = false;
            }
            else
            {
                mainForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            }

            Application.Run(mainForm);
        }
    }
}
