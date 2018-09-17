﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimecardsCore;
using core = TimecardsCore.Models;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void DefaultCodesTest()
        {
            const string SECOND = "Second Description";
            const string PREEXISTING = "Preexisting Description";

            var backup = BackupOfConfigurationDefaultCodes();

            Configuration.DefaultCodes.Clear();
            Configuration.DefaultCodes["00100"] = "First Description";
            Configuration.DefaultCodes["00200"] = SECOND;
            Configuration.DefaultCodes["00300"] = "Third Description";

            string error = null;

            try
            {
                core.Activity activity;

                activity = new core.Activity();
                activity.Code = "00000";
                Assert.AreEqual(string.Empty, activity.Description, "Code not in dictionary should not assign a value");

                activity = new core.Activity();
                activity.Code = "00200";
                Assert.AreEqual(SECOND, activity.Description, "Assigning code did not set default description");

                activity = new core.Activity();
                activity.Description = PREEXISTING;
                activity.Code = "00200";
                Assert.AreEqual(PREEXISTING, activity.Description, "Assigning code overwrote preexisting description");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                RestoreConfigurationDefaultCodesFromBackup(backup);
            }

            if (error != null)
                throw new Exception($"Error encountered: {error}");
        }

        [TestMethod]
        public void LoadSaveTest()
        {
            var backupRound = Configuration.RoundCurrentTimeToMinutes;
            var backupMask = Configuration.TicketNumberMask;
            var backupCodes = BackupOfConfigurationDefaultCodes();

            string error = null;

            try
            {
                Configuration.RoundCurrentTimeToMinutes = 777;
                Configuration.TicketNumberMask = "777";
                Configuration.DefaultCodes.Clear();
                Configuration.DefaultCodes["7771"] = "777771";
                Configuration.DefaultCodes["7772"] = "777772";
                Configuration.DefaultCodes["7773"] = "777773";
                Configuration.DefaultCodes["7774"] = "777774";
                Configuration.DefaultCodes["7775"] = "777775";

                Assert.AreEqual(777, Configuration.RoundCurrentTimeToMinutes, "Configuration value didn't set");
                Assert.AreEqual("777", Configuration.TicketNumberMask, "Configuration value didn't set");
                Assert.AreEqual(5, Configuration.DefaultCodes.Count, "Configuration value didn't set");
                Assert.AreEqual("777771", Configuration.DefaultCodes["7771"], "Configuration value didn't set");
                Assert.AreEqual("777772", Configuration.DefaultCodes["7772"], "Configuration value didn't set");
                Assert.AreEqual("777773", Configuration.DefaultCodes["7773"], "Configuration value didn't set");
                Assert.AreEqual("777774", Configuration.DefaultCodes["7774"], "Configuration value didn't set");
                Assert.AreEqual("777775", Configuration.DefaultCodes["7775"], "Configuration value didn't set");

                Configuration.Save();

                Configuration.RoundCurrentTimeToMinutes = 888;
                Configuration.TicketNumberMask = "888";
                Configuration.DefaultCodes.Clear();
                Configuration.DefaultCodes["888"] = "88888";

                Configuration.Load();

                Assert.AreEqual(777, Configuration.RoundCurrentTimeToMinutes, "Configuration value didn't load");
                Assert.AreEqual("777", Configuration.TicketNumberMask, "Configuration value didn't load");
                Assert.AreEqual(5, Configuration.DefaultCodes.Count, "Configuration value didn't load");
                Assert.AreEqual("777771", Configuration.DefaultCodes["7771"], "Configuration value didn't load");
                Assert.AreEqual("777772", Configuration.DefaultCodes["7772"], "Configuration value didn't load");
                Assert.AreEqual("777773", Configuration.DefaultCodes["7773"], "Configuration value didn't load");
                Assert.AreEqual("777774", Configuration.DefaultCodes["7774"], "Configuration value didn't load");
                Assert.AreEqual("777775", Configuration.DefaultCodes["7775"], "Configuration value didn't load");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                Configuration.RoundCurrentTimeToMinutes = backupRound;
                Configuration.TicketNumberMask = backupMask;
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
