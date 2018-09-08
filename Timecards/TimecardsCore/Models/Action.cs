using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Action
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public readonly int StartMinute;

        public Action(DateTime date)
        {
            var round = Math.Max(1, Configuration.RoundTimeToMinutes);

            StartMinute = (int)(DateTime.Now - date).TotalMinutes;
            StartMinute = (int)Math.Round((double)StartMinute / round) * round;

            var hour = (StartMinute / 60) % 24;
            var minute = StartMinute % 60;

            Time = string.Format($"{hour:D2}:{minute:D2}");
        }

        public Action(DateTime date, string code) : this(date)
        {
            Code = code;
            if (Configuration.DefaultCodes.ContainsKey(code))
                Description = Configuration.DefaultCodes[code];
        }

        public Action(DateTime date, string code, string description) : this(date, code)
        {
            Description = description;
        }
    }
}
