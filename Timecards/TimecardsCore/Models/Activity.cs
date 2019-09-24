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

        public int ID {
            get => ID;
            set
            {
                ID = value;
                IsDirty = true;
            }
        }

        public int TimecardID
        {
            get => TimecardID;
            set
            {
                TimecardID = value;
                IsDirty = true;
            }
        }

        public string Code
        {
            get => Code;
            set
            {
                Code = value;
                if (string.IsNullOrWhiteSpace(Description) && Configuration.DefaultCodes.ContainsKey(Code))
                    Description = Configuration.DefaultCodes[Code];
                IsDirty = true;
            }
        }

        public string Description
        {
            get => Description;
            set
            {
                Description = value;
                IsDirty = true;
            }
        }

        public string Time
        {
            get => Time;
            set
            {
                Time = PadTime(value);
                ComputeStartMinuteFromTime();
                IsDirty = true;
            }
        }

        public int StartMinute
        {
            get => StartMinute;
            set
            {
                StartMinute = value;
                ComputeTimeFromStartMinute();
                IsDirty = true;
            }
        }

        public bool IsAfterMidnight
        {
            get => IsAfterMidnight;
            set
            {
                IsAfterMidnight = value;
                ComputeStartMinuteFromTime();
                IsDirty = true;
            }
        }

        public bool IsDirty { get; private set; }

        #endregion

        #region Private properties

        private bool _isLoading;

        #endregion

        #region Constructors

        public Activity()
        {
            ID = 0;

            _isLoading = true;
            StartMinute = 0;
            Time = "00" + TIMESEP + "00";
            IsAfterMidnight = false;
            _isLoading = false;

            Code = string.Empty;
            Description = string.Empty;

            IsDirty = false;
        }

        public Activity(string code) : this()
        {
            Code = code;
            IsDirty = false;
        }

        public Activity(string code, string description) : this(code)
        {
            Description = description;
            IsDirty = false;
        }

        public Activity(string code, string description, string time) : this(code, description)
        {
            Time = time;
            IsDirty = false;
        }

        public Activity(string code, string description, int startMinute) : this(code, description)
        {
            StartMinute = startMinute;
            IsDirty = false;
        }

        public Activity(int id, string code) : this()
        {
            ID = id;
            Code = code;
            IsDirty = false;
        }

        public Activity(int id, string code, string description) : this(id, code)
        {
            Description = description;
            IsDirty = false;
        }

        public Activity(int id, string code, string description, string time) : this(id, code, description)
        {
            Time = time;
            IsDirty = false;
        }

        public Activity(int id, string code, string description, int startMinute) : this(id, code, description)
        {
            StartMinute = startMinute;
            IsDirty = false;
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

        private (int, int) Normalize((int, int) value)
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
            if (_isLoading)
                return;

            var (hour, minute) = ParseTime(Time);
            StartMinute = hour * 60 + minute + (IsAfterMidnight ? 1440 : 0);
        }

        private void ComputeTimeFromStartMinute()
        {
            if (_isLoading)
                return;

            var hour = (StartMinute / 60) % 24;
            var minute = StartMinute % 60;
            IsAfterMidnight = (StartMinute > 24 * 60);

            Time = string.Format($"{hour:D2}:{minute:D2}");
        }

        #endregion
    }
}
