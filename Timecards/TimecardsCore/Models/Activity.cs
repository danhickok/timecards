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

        public int ID { get; set; }
        public int TimecardID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        private string _time;
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                ComputeStartMinuteFromTime();
            }
        }

        private int _startMinute;
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

        public Activity()
        {
            _startMinute = 0;
            _time = null;
        }

        public Activity(string code)
        {
            Code = code;
            if (string.IsNullOrWhiteSpace(Description) && Configuration.DefaultCodes.ContainsKey(code))
                Description = Configuration.DefaultCodes[code];
        }

        public Activity(string code, string description) : this(code)
        {
            Description = description;
        }

        public Activity(string code, string description, string time) : this(code, description)
        {
            Time = time;
        }

        public Activity(string code, string description, int startMinute) : this(code, description)
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
                StartMinute = hour * 60 + minute;
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
