using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Activity
    {
        private const string TIMESEP = ":";

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
                _time = PadTime(value);
                ComputeStartMinuteFromTime();
            }
        }

        private int _startMinute;
        public int StartMinute
        {
            get => _startMinute;
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
            _time = "00" + TIMESEP + "00";
            Code = string.Empty;
            Description = string.Empty;
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

        private (int, int) ParseTime(string time)
        {
            int hour = 0;
            int minute = 0;

            if (!string.IsNullOrWhiteSpace(time))
            {
                var pos = time.IndexOf(TIMESEP);
                if (pos < 0)
                {
                    if (int.TryParse(time, out int value))
                    {
                        hour = value / 100;
                        minute = value % 100;
                    }
                }
                else
                {
                    int.TryParse(time.Substring(0, pos), out hour);
                    int.TryParse(time.Substring(pos + 1), out minute);
                }
            }

            return (hour, minute);
        }

        private (int, int) Normalize(ValueTuple<int, int> value)
        {
            var (hour, minute) = value;

            while (minute < 0)
            {
                minute += 60;
                hour--;
            }
            while (minute >= 60)
            {
                minute -= 60;
                hour++;
            }

            return (hour, minute);
        }

        private string PadTime(string time)
        {
            var (hour, minute) = Normalize(ParseTime(time));
            return $"{hour:D2}{TIMESEP}{minute:D2}";
        }

        private void ComputeStartMinuteFromTime()
        {
            var (hour, minute) = ParseTime(_time);
            _startMinute = hour * 60 + minute;
        }

        private void ComputeTimeFromStartMinute()
        {
            var hour = (_startMinute / 60) % 24;
            var minute = _startMinute % 60;
            _time = string.Format($"{hour:D2}:{minute:D2}");
        }

        #endregion
    }
}
