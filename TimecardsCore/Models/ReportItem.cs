using System;

namespace TimecardsCore.Models
{
    /// <summary>
    /// This class defines a row in the report: one code, its date range, and elapsed time in minutes
    /// </summary>
    public class ReportItem
    {
        public string Code { get; set; }
        public DateTime EarliestDate { get; set; }
        public DateTime LatestDate { get; set; }
        public int TotalMinutes { get; set; }
    }
}
