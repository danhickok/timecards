using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core = TimecardsCore.Models;

namespace TimecardsData
{
    public static class MappingMethods
    {
        #region Timecard methods

        public static core.Timecard ToCore(this Timecard data)
        {
            var core = new core.Timecard
            {
                ID = data.ID,
                Date = data.Date,
            };

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
            var core = new core.Activity
            {
                ID = data.ID,
                TimecardID = data.TimecardID,
                Code = data.Code,
                Description = data.Description,
                Time = data.Time,
            };

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
                Time = core.Time,
            };
        }

        public static void UpdateFromCore(this Activity data, core.Activity core)
        {
            data.ID = core.ID;
            data.Code = core.Code;
            data.Description = core.Description;
            data.Time = core.Time;
        }

        public static void UpdateFromData(this core.Activity core, Activity data)
        {
            core.ID = data.ID;
            core.Code = data.Code;
            core.Description = data.Description;
            core.Time = data.Time;
        }

        #endregion
    }
}
