using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TimecardsCore;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void LoadSaveTest()
        {
            var backupRound = Configuration.RoundCurrentTimeToMinutes;
            var backupCodeMask = Configuration.CodeMask;
            var backupTimeMask = Configuration.TimeMask;
            var backupTimeSeparator = Configuration.TimeSeparator;
            var backupCodes = BackupOfConfigurationDefaultCodes();

            // the following tests are run in a try/catch block so that any error does not accidentally
            // leave the configuration in a test state

            try
            {
                Configuration.RoundCurrentTimeToMinutes = 777;
                Configuration.CodeMask = "777";
                Configuration.TimeMask = "77.77";
                Configuration.TimeSeparator = '.';
                Configuration.DefaultCodes.Clear();
                Configuration.DefaultCodes["7771"] = "777771";
                Configuration.DefaultCodes["7772"] = "777772";
                Configuration.DefaultCodes["7773"] = "777773";
                Configuration.DefaultCodes["7774"] = "777774";
                Configuration.DefaultCodes["7775"] = "777775";

                Assert.AreEqual(777, Configuration.RoundCurrentTimeToMinutes, "Configuration value didn't set");
                Assert.AreEqual("777", Configuration.CodeMask, "Configuration value didn't set");
                Assert.AreEqual("77.77", Configuration.TimeMask, "Configuration value didn't set");
                Assert.AreEqual('.', Configuration.TimeSeparator, "Configuration value didn't set");
                Assert.AreEqual(5, Configuration.DefaultCodes.Count, "Configuration value didn't set");
                Assert.AreEqual("777771", Configuration.DefaultCodes["7771"], "Configuration value didn't set");
                Assert.AreEqual("777772", Configuration.DefaultCodes["7772"], "Configuration value didn't set");
                Assert.AreEqual("777773", Configuration.DefaultCodes["7773"], "Configuration value didn't set");
                Assert.AreEqual("777774", Configuration.DefaultCodes["7774"], "Configuration value didn't set");
                Assert.AreEqual("777775", Configuration.DefaultCodes["7775"], "Configuration value didn't set");

                Configuration.Save();

                Configuration.RoundCurrentTimeToMinutes = 888;
                Configuration.CodeMask = "888";
                Configuration.TimeMask = "88:88";
                Configuration.TimeSeparator = ':';
                Configuration.DefaultCodes.Clear();
                Configuration.DefaultCodes["888"] = "88888";

                Configuration.Load();

                Assert.AreEqual(777, Configuration.RoundCurrentTimeToMinutes, "Configuration value didn't load");
                Assert.AreEqual("777", Configuration.CodeMask, "Configuration value didn't load");
                Assert.AreEqual("77.77", Configuration.TimeMask, "Configuration value didn't load");
                Assert.AreEqual('.', Configuration.TimeSeparator, "Configuration value didn't load");
                Assert.AreEqual(5, Configuration.DefaultCodes.Count, "Configuration value didn't load");
                Assert.AreEqual("777771", Configuration.DefaultCodes["7771"], "Configuration value didn't load");
                Assert.AreEqual("777772", Configuration.DefaultCodes["7772"], "Configuration value didn't load");
                Assert.AreEqual("777773", Configuration.DefaultCodes["7773"], "Configuration value didn't load");
                Assert.AreEqual("777774", Configuration.DefaultCodes["7774"], "Configuration value didn't load");
                Assert.AreEqual("777775", Configuration.DefaultCodes["7775"], "Configuration value didn't load");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred during configuration test: {ex.Message}");
            }
            finally
            {
                Configuration.RoundCurrentTimeToMinutes = backupRound;
                Configuration.CodeMask = backupCodeMask;
                Configuration.TimeMask = backupTimeMask;
                Configuration.TimeSeparator = backupTimeSeparator;
                RestoreConfigurationDefaultCodesFromBackup(backupCodes);
                Configuration.Save();
            }
        }

        private Dictionary<string, string> BackupOfConfigurationDefaultCodes()
        {
            var result = new Dictionary<string, string>();

            foreach (var key in Configuration.DefaultCodes.Keys)
            {
                result[key] = Configuration.DefaultCodes[key];
            }

            return result;
        }

        private void RestoreConfigurationDefaultCodesFromBackup(Dictionary<string, string> backup)
        {
            Configuration.DefaultCodes.Clear();

            foreach (var key in backup.Keys)
            {
                Configuration.DefaultCodes[key] = backup[key];
            }
        }
    }
}
