using System;
using System.Collections.Generic;
using TimecardsCore.Models;

namespace TimecardsCore.Interfaces
{
    public interface IRepository
    {
        int GetTimecardCount();

        List<Timecard> GetTimecards(int offset, int limit, bool descending);

        Timecard GetTimecard(int id);

        Timecard GetTimecard(DateTime date);

        void SaveTimecard(Timecard timecard);

        void DeleteTimecard(int id);

        void GetActivities(Timecard timecard);

        void SaveActivities(Timecard timecard);

        Activity GetActivity(int id);

        void SaveActivity(Activity activity);

        void DeleteActivity(int id);

        void DeleteAllTimecards();

        List<ReportItem> GetReport(DateTime startDate, DateTime endDate);
    }
}
