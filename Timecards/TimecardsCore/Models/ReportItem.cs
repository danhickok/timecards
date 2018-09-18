using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class ReportItem
    {
        public string Code { get; set; }
        public DateTime EarliestDate { get; set; }
        public DateTime LatestDate { get; set; }
        public int TotalMinutes { get; set; }
        public int TotalHours { get; set; }
    }
}
