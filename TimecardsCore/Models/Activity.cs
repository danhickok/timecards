using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TimecardsCore.Exceptions;

namespace TimecardsCore.Models
{
    /// <summary>
    /// This class holds an activity for a timecard: what was done, when it was started, and what for (the code)
    /// </summary>
    public class Activity
    {
        private readonly char TIMESEP;
        private readonly bool USE24HOUR;

        #region Public properties

        private int _id;
        [JsonIgnore]
        public int ID
        {
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
        [JsonIgnore]
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
        [JsonIgnore]
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

        [JsonIgnore]
        public bool IsDirty { get; private set; }

        [JsonIgnore]
        private DateTime? _timecardDate { get; set; }

        #endregion

        #region Constructors

        public Activity()
        {
            TIMESEP = Configuration.TimeSeparator;
            USE24HOUR = Configuration.Use24HourTime;

            _id = 0;
            _timecardID = 0;
            _isAfterMidnight = false;
            _code = string.Empty;
            _description = string.Empty;
            _time = "";

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

        #region Public methods

        public void SetTimecardDate(DateTime date, bool importMode = false)
        {
            _timecardDate = date;

            if (!importMode)
            {
                IsAfterMidnight = (Configuration.CurrentDateTime.Date > _timecardDate.Value.Date);
            }
        }

        public override string ToString()
        {
            return $"ID={_id}, TCID={_timecardID}, Code=\"{_code}\", Description=\"{_description}\", Time=\"{_time}\", StartMinute={_startMinute}, IsAfterMidnight={_isAfterMidnight}";
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
                    _ = int.TryParse(time.AsSpan(0, pos), out hour);
                    _ = int.TryParse(Regex.Replace(time[(pos + 1)..], "[^0-9]", ""), out minute);
                }

                if (!USE24HOUR && time.EndsWith("a", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (hour == 12)
                        hour = 0;
                }

                if (!USE24HOUR && time.EndsWith("p", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (hour < 12)
                        hour += 12;
                }
            }

            return (hour, minute);
        }

        private static (int, int) Normalize((int, int) value)
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

            while (hour < 0)
                hour += 24;

            hour %= 24;

            return (hour, minute);
        }

        private string PadTime(string time)
        {
            var (hour, minute) = Normalize(ParseTime(time));
            return FormatTime(hour, minute);
        }

        private void ComputeStartMinuteFromTime()
        {
            var (hour, minute) = Normalize(ParseTime(Time));
            _startMinute = hour * 60 + minute + (_isAfterMidnight ? 24 * 60 : 0);
        }

        private void ComputeTimeFromStartMinute()
        {
            var hour = (_startMinute / 60) % 24;
            var minute = _startMinute % 60;
            _isAfterMidnight = (_startMinute > 24 * 60);

            _time = FormatTime(hour, minute);
        }

        private string FormatTime(int hour, int minute)
        {
            string time;

            if (USE24HOUR)
            {
                time = $"{hour:D2}{TIMESEP}{minute:D2}";
            }
            else
            {
                var hour2 = hour % 12;
                if (hour2 == 0)
                    hour2 = 12;

                time =
                    (hour2 < 10 ? " " : "") +
                    $"{hour2:D}{TIMESEP}{minute:D2}" +
                    (hour >= 12 ? "p" : "a");
            }

            return time;
        }

        #endregion
    }
}
