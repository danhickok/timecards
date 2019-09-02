using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using core = TimecardsCore;
using data = TimecardsData;
using ioc = TimecardsIOC;
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

            core.Configuration.Load();

            var factory = new ioc.Factory();
            factory.Register<core.Interfaces.IAppConstants>(typeof(ProductionAppConstants), true);
            factory.Register<core.Interfaces.IRepository>(typeof(data.Repository), true,
                typeof(core.Interfaces.IAppConstants));

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

            Application.Run(mainForm);
        }
    }
}
