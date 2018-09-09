using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Activity
    {
        #region Public properties

        public readonly DateTime Date;

        public string Code { get; set; }
        public string Description { get; set; }

        private string _time = null;
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                ComputeStartMinuteFromTime();
            }
        }

        private int _startMinute = 0;
        public int StartMinute
        {
            get => StartMinute;
            set
            {
                _startMinute = value;
                ComputeTimeFromStartMinute();
            }
        }

        #endregion

        #region Constructors

        public Activity(DateTime date) => Date = date;

        public Activity(DateTime date, string code) : this(date)
        {
            Code = code;
            if (string.IsNullOrWhiteSpace(Description) && Configuration.DefaultCodes.ContainsKey(code))
                Description = Configuration.DefaultCodes[code];
        }

        public Activity(DateTime date, string code, string description) : this(date, code)
        {
            Description = description;
        }

        public Activity(DateTime date, string code, string description, string time) : this(date, code, description)
        {
            Time = time;
        }

        public Activity(DateTime date, string code, string description, int startMinute) : this(date, code, description)
        {
            StartMinute = startMinute;
        }

        #endregion

        #region Private methods

        private void ComputeStartMinuteFromTime()
        {
            if (
                    Time != null &&
                    Time.IndexOf(":") >= 0 &&
                    int.TryParse(Time.Substring(0, Time.IndexOf(":")), out int hour) &&
                    int.TryParse(Time.Substring(Time.IndexOf(":") + 1), out int minute)
                )
            {
                var now = new DateTime(Date.Year, Date.Month, Date.Day, hour, minute, 0);
                StartMinute = (int)(now - Date).TotalMinutes;
            }
        }

        private void ComputeTimeFromStartMinute()
        {
            var hour = (StartMinute / 60) % 24;
            var minute = StartMinute % 60;
            Time = string.Format($"{hour:D2}:{minute:D2}");
        }

        #endregion
    }
}
