using System.Linq;
using TimecardsCore.Models;
using tm = TimecardsCore.Models;

namespace TimecardsData
{
    /// <summary>
    /// This class holds extension methods for mapping between Core classes and their corresponding DTO classes
    /// </summary>
    public static class MappingMethods
    {
        #region Timecard methods

        public static tm.Timecard ToCore(this Timecard data)
        {
            var core = new tm.Timecard(data.ID, data.Date);

            if (data.Activities != null)
                core.Activities.AddRange(data.Activities.Select(a => a.ToCore()));

            return core;
        }

        public static Timecard ToData(this tm.Timecard core)
        {
            return new Timecard
            {
                ID = core.ID,
                Date = core.Date,
            };
        }

        public static void UpdateFromCore(this Timecard data, tm.Timecard core)
        {
            data.ID = core.ID;
            data.Date = core.Date;
        }

        public static void UpdateFromData(this tm.Timecard core, Timecard data)
        {
            core.ID = data.ID;
            core.Date = data.Date;
        }

        #endregion

        #region Activity methods

        public static tm.Activity ToCore(this Activity data)
        {
            var core = new tm.Activity(data.ID, data.TimecardID, data?.Code ?? "", data?.Description ?? "", data?.StartMinute ?? 0);

            return core;
        }

        public static Activity ToData(this tm.Activity core)
        {
            return new Activity
            {
                ID = core.ID,
                TimecardID = core.TimecardID,
                Code = core.Code,
                Description = core.Description,
                StartMinute = core.StartMinute,
            };
        }

        public static void UpdateFromCore(this Activity data, tm.Activity core)
        {
            data.ID = core.ID;
            data.TimecardID = core.TimecardID;
            data.Code = core.Code;
            data.Description = core.Description;
            data.StartMinute = core.StartMinute;
        }

        public static void UpdateFromData(this tm.Activity core, Activity data)
        {
            core.ID = data.ID;
            core.TimecardID = data.TimecardID;
            core.Code = data?.Code ?? "";
            core.Description = data?.Description ?? "";
            core.StartMinute = data?.StartMinute ?? 0;
        }

        #endregion
    }
}
