using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Models;

namespace TimecardsCore.Interfaces
{
    public interface IRepository
    {
        List<Timecard> GetTimecards();

        Timecard GetTimecard(int id);

        Timecard GetTimecard(DateTime date);

        void SaveTimecard(Timecard timecard);

        void DeleteTimecard(int id);

        void GetActivities(Timecard timecard);

        void SaveActivities(Timecard timecard);

        Activity GetActivity(int id);

        void SaveActivity(Activity activity);

        void DeleteActivity(int id);

        List<ReportItem> GetReport(DateTime startDate, DateTime endDate);
    }
}
