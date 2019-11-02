using System;
using TimecardsCore.Exceptions;

namespace TimecardsCore.Models
{
    public class Activity
    {
        private readonly char TIMESEP;

        #region Public properties

        private int _id;
        public int ID {
            get => _id;
            set
            {
                if (value < 0)
                    throw new InvalidValueException();

                _id = value;
                IsDirty = true;
            }
        }

        private int _timecardID;
        public int TimecardID
        {
            get => _timecardID;
            set
            {
                if (value < 0)
                    throw new InvalidValueException();

                _timecardID = value;
                IsDirty = true;
            }
        }

        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                _code = value ?? throw new NullNotAllowedException();
                IsDirty = true;
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value ?? throw new NullNotAllowedException();
                IsDirty = true;
            }
        }

        private string _time;
        public string Time
        {
            get => _time;
            set
            {
                _time = PadTime(value ?? throw new NullNotAllowedException());
                ComputeStartMinuteFromTime();
                IsDirty = true;
            }
        }

        private int _startMinute;
        public int StartMinute
        {
            get => _startMinute;
            set
            {
                if (value < 0 || value >= 2 * 24 * 60)
                    throw new InvalidValueException();

                _startMinute = value;
                ComputeTimeFromStartMinute();
                IsDirty = true;
            }
        }

        private bool _isAfterMidnight;
        public bool IsAfterMidnight
        {
            get => _isAfterMidnight;
            set
            {
                _isAfterMidnight = value;
                ComputeStartMinuteFromTime();
                IsDirty = true;
            }
        }

        public bool IsDirty { get; private set; }

        public Func<DateTime> RequestTimecardDate; //TODO: when assigning this property, recalculate the start minute

        #endregion

        #region Constructors

        public Activity()
        {
            TIMESEP = Configuration.TimeSeparator;

            _id = 0;
            _timecardID = 0;
            _isAfterMidnight = false;
            _code = string.Empty;
            _description = string.Empty;

            var now = Configuration.CurrentDateTime;
            var mins = now.Hour * 60 + now.Minute;

            StartMinute = (int)(Math.Round(mins / (double)Configuration.RoundCurrentTimeToMinutes)
                * Configuration.RoundCurrentTimeToMinutes);

            IsDirty = false;
        }

        public Activity(string code) : this()
        {
            _code = code;
            IsDirty = false;
        }

        public Activity(string code, string description) : this(code)
        {
            _description = description;
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

        public Activity(int id, int timecardID, string code) : this()
        {
            _id = id;
            _timecardID = timecardID;
            _code = code;
            IsDirty = false;
        }

        public Activity(int id, int timecardID, string code, string description) : this(id, timecardID, code)
        {
            _description = description;
            IsDirty = false;
        }

        public Activity(int id, int timecardID, string code, string description, string time) : this(id, timecardID, code, description)
        {
            Time = time;
            IsDirty = false;
        }

        public Activity(int id, int timecardID, string code, string description, int startMinute) : this(id, timecardID, code, description)
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
            var (hour, minute) = ParseTime(Time);
            _startMinute = hour * 60 + minute + (_isAfterMidnight ? 24 * 60 : 0);
        }

        private void ComputeTimeFromStartMinute()
        {
            var hour = (_startMinute / 60) % 24;
            var minute = _startMinute % 60;
            _isAfterMidnight = (_startMinute > 24 * 60);

            _time = string.Format($"{hour:D2}:{minute:D2}");
        }

        #endregion
    }
}
