using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Logic
{
    public class BulkLogic
    {
        public string Export(DateTime? StartDate, DateTime? EndDate, DataFormat format)
        {
            var result = new StringBuilder();

            //TODO: retrieve the data
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
