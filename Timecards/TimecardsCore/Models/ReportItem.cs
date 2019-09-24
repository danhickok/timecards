using System;

namespace TimecardsCore.Models
{
    public class ReportItem
    {
        public string Code { get; set; }
        public DateTime EarliestDate { get; set; }
        public DateTime LatestDate { get; set; }
        public int TotalMinutes { get; set; }
    }
}
