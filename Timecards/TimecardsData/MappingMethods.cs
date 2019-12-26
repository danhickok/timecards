using System.Linq;
using core = TimecardsCore.Models;

namespace TimecardsData
{
    /// <summary>
    /// This class holds extension methods for mapping between Core classes and their corresponding DTO classes
    /// </summary>
    public static class MappingMethods
    {
        #region Timecard methods

        public static core.Timecard ToCore(this Timecard data)
        {
            var core = new core.Timecard(data.ID, data.Date);

            if (data.Activities != null)
                core.Activities.AddRange(data.Activities.Select(a => a.ToCore()));

            return core;
        }

        public static Timecard ToData(this core.Timecard core)
        {
            return new Timecard
            {
                ID = core.ID,
                Date = core.Date,
            };
        }

        public static void UpdateFromCore(this Timecard data, core.Timecard core)
        {
            data.ID = core.ID;
            data.Date = core.Date;
        }

        public static void UpdateFromData(this core.Timecard core, Timecard data)
        {
            core.ID = data.ID;
            core.Date = data.Date;
        }

        #endregion

        #region Activity methods

        public static core.Activity ToCore(this Activity data)
        {
            var core = new core.Activity(data.ID, data.TimecardID, data.Code, data.Description, data.StartMinute);

            return core;
        }

        public static Activity ToData(this core.Activity core)
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

        public static void UpdateFromCore(this Activity data, core.Activity core)
        {
            data.ID = core.ID;
            data.TimecardID = core.TimecardID;
            data.Code = core.Code;
            data.Description = core.Description;
            data.StartMinute = core.StartMinute;
        }

        public static void UpdateFromData(this core.Activity core, Activity data)
        {
            core.ID = data.ID;
            core.TimecardID = data.TimecardID;
            core.Code = data.Code;
            core.Description = data.Description;
            core.StartMinute = data.StartMinute;
        }

        #endregion
    }
}
