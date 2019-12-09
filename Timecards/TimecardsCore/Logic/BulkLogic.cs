using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public class BulkLogic
    {
        private readonly IFactory _factory;

        public BulkLogic(IFactory factory)
        {
            _factory = factory;
        }

        public string Export(DateTime? startDate, DateTime? endDate, DataFormat format)
        {
            var result = new StringWriter();

            var repo = _factory.Resolve<IRepository>();

            // retrieve the timecards to be exported
            var tcList = repo.GetTimecards(startDate, endDate);

            //TODO: finish
            //TODO: transform the data

            return result.ToString();
        }

        public void Import(string path, DataFormat format)
        {
            //TODO: read in the data
            //TODO: transform the data
            //TODO: store the data, raising an event indicating progress
        }

        public enum DataFormat
        {
            [Description("Comma-delimited text")]
            CommaDelimitedText,

            [Description("Tab-delimited text")]
            TabDelimitedText,

            [Description("JSON")]
            JSON,

            [Description("XML")]
            XML
        }
    }
}
