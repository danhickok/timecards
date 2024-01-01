using TimecardsCore.Interfaces;
using TimecardsCore.Models;

namespace TimecardsCore.Logic
{
    /// <summary>
    /// This class is used for retrieving a list of report objects that show elapsed time per code
    /// </summary>
    public class ReportLogic
    {
        private readonly IFactory _factory;

        #region Constructor

        public ReportLogic(IFactory factory)
        {
            _factory = factory;
        }

        #endregion

        /// <summary>
        /// Retrieves a list of ReportItem objects
        /// </summary>
        /// <param name="start">Low end of date range</param>
        /// <param name="end">High end of date range</param>
        /// <returns>List of ReportItem objects showing elapsed time for each code in date range</returns>
        public List<ReportItem> GetReport(DateTime start, DateTime end)
        {
            var repo = _factory.Resolve<IRepository>();
            return repo.GetReport(start, end);
        }
    }
}
