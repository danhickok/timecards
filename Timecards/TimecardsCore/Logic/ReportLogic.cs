using System;
using System.Collections.Generic;
using TimecardsCore.Models;
using TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public class ReportLogic
    {
        private readonly IFactory _factory;

        public ReportLogic(IFactory factory)
        {
            _factory = factory;
        }

        public List<ReportItem> GetReport(DateTime start, DateTime end)
        {
            var repo = _factory.Resolve<IRepository>();
            return repo.GetReport(start, end);
        }
    }
}
