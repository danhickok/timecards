using System;
using System.Windows.Forms;
using ic = TimecardsIOC;
using tc = TimecardsCore;
using td = TimecardsData;
using ui = TimecardsUI;

namespace Timecards
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

            tc.Configuration.Load();

            var factory = new ic.Factory();
            factory.Register<tc.Interfaces.IAppConstants>(typeof(ProductionAppConstants), true);
            factory.Register<tc.Interfaces.IRepository>(typeof(td.Repository), false,
                typeof(tc.Interfaces.IAppConstants));

            var mainForm = new ui.MainForm
            {
                Factory = factory
            };

            if (ui.MainFormSettings.HaveBeenSet)
            {
                mainForm.InitialPositioning = true;

                mainForm.Top = ui.MainFormSettings.Top;
                mainForm.Left = ui.MainFormSettings.Left;
                mainForm.Height = ui.MainFormSettings.Height;
                mainForm.Width = ui.MainFormSettings.Width;

                mainForm.InitialPositioning = false;
            }
            else
            {
                mainForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            }

            try
            {
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception: {e.Message}\n\n{e.StackTrace}", "Timecards: Untrapped Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (factory != null)
                    factory.Dispose();
            }
        }
    }
}
