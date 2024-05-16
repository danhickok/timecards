using TM = TimecardsCore.Models;

namespace TimecardsCore.Interfaces
{
    /// <summary>
    /// The Repository class abstracts all persistence operations from the rest of the application
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Returns the number of timecards currently in the database
        /// </summary>
        /// <returns>Count of the number of timecards available</returns>
        int GetTimecardCount();

        /// <summary>
        /// Gets a shallow list of timecards in date order based on paging parameters; use this for navigation in the UI
        /// </summary>
        /// <param name="offset">Number of timecards to skip</param>
        /// <param name="limit">Page size for this list</param>
        /// <param name="descending">True if ordering by descending date</param>
        /// <returns>Shallow list of timecards that do not include any activities</returns>
        List<TM.Timecard> GetTimecards(int offset, int limit, bool descending);

        /// <summary>
        /// Gets a deep list of timecards that fall within the given date range; use this for exporting data
        /// </summary>
        /// <param name="startDate">Lowest date in range</param>
        /// <param name="endDate">Highest date in range</param>
        /// <returns>Deep list of timecards, each containing their activities</returns>
        List<TM.Timecard> GetTimecards(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Retrieves a timecard by ID or null if it doesn't exist
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Timecard including its activities</returns>
        TM.Timecard? GetTimecard(int id);

        /// <summary>
        /// Finds the timecard closest to the given date or null if none found
        /// </summary>
        /// <param name="date">Target date for timecard</param>
        /// <param name="after">True if found timecard has a date later than given one</param>
        /// <returns>Timecard including its activities</returns>
        TM.Timecard? GetNearestTimecard(DateTime date, bool after);

        /// <summary>
        /// Saves the given timecard in the database, along with its activities
        /// </summary>
        /// <param name="timecard">Timecard to be saved</param>
        void SaveTimecard(TM.Timecard timecard);

        /// <summary>
        /// Removes a timecard from the database
        /// </summary>
        /// <param name="id">Primary key of the timecard</param>
        void DeleteTimecard(int id);

        /// <summary>
        /// Retrieves all the activities for a given timecard
        /// </summary>
        /// <param name="timecard">The timecard</param>
        void GetActivities(TM.Timecard timecard);

        /// <summary>
        /// Saves any updated activities for the given timecard
        /// </summary>
        /// <param name="timecard">The timecard</param>
        void SaveActivities(TM.Timecard timecard);

        /// <summary>
        /// Retrieves a single activity by its ID
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Activity</returns>
        TM.Activity? GetActivity(int id);

        /// <summary>
        /// Saves a single activity to the database
        /// </summary>
        /// <param name="activity">Activity</param>
        void SaveActivity(TM.Activity activity);

        /// <summary>
        /// Deletes an activity by its ID
        /// </summary>
        /// <param name="id">Primary key</param>
        void DeleteActivity(int id);

        /// <summary>
        /// Deletes all timecards and their activities from the database
        /// </summary>
        void DeleteAllTimecards();

        /// <summary>
        /// Retrieve results of report query
        /// </summary>
        /// <param name="startDate">Low value of date range</param>
        /// <param name="endDate">High value of date range</param>
        /// <returns>List of ReportItem objects that show elapsed time by code</returns>
        List<TM.ReportItem> GetReport(DateTime startDate, DateTime endDate);
    }
}
