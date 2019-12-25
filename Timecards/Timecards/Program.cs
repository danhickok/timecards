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

            // load the application configuration from the settings file
            tc.Configuration.Load();

            // create the inversion-of-control container and give it the production constants object
            // (this establishes the path to the database for normal operation, among other things)
            var factory = new ic.Factory();
            factory.Register<tc.Interfaces.IAppConstants>(typeof(ProductionAppConstants), true);
            factory.Register<tc.Interfaces.IRepository>(typeof(td.Repository), false,
                typeof(tc.Interfaces.IAppConstants));

            // create the main form object and establish its last known position, if available
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

            // open the main form and run the application
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
