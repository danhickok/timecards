using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using TimecardsCore.ExtensionMethods;
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
            var repo = _factory.Resolve<IRepository>();

            // retrieve the timecards to be exported
            var tcList = repo.GetTimecards(startDate, endDate);

            using (var sw = new StringWriter())
            {
                // transform the data
                switch (format)
                {
                    case DataFormat.CommaDelimitedText:
                        sw.WriteLine("Date,Code,Description,Time,IsAfterMidnight");
                        foreach (var tc in tcList)
                        {
                            foreach (var ac in tc.Activities)
                            {
                                sw.WriteLine($"\"{tc.Date:yyyy-MM-dd}\",\"{ac.Code}\",\"{ac.Description}\",\"{ac.Time}\",{ac.IsAfterMidnight}");
                            }
                        }
                        break;

                    case DataFormat.TabDelimitedText:
                        sw.WriteLine("Date\tCode\tDescription\tTime\tIsAfterMidnight");
                        foreach (var tc in tcList)
                        {
                            foreach (var ac in tc.Activities)
                            {
                                sw.WriteLine($"{tc.Date:yyyy-MM-dd}\t{ac.Code}\t{ac.Description}\t{ac.Time}\t{ac.IsAfterMidnight}");
                            }
                        }
                        break;

                    case DataFormat.JSON:
                        sw.WriteLine(JsonConvert.SerializeObject(tcList));
                        break;

                    case DataFormat.XML:
                        XmlAttribute attr;

                        var xdoc = new XmlDocument();

                        var rootNode = xdoc.CreateElement("Timecards");
                        xdoc.AppendChild(rootNode);

                        foreach (var tc in tcList)
                        {
                            var tcNode = xdoc.CreateElement("Timecard");
                            attr = xdoc.CreateAttribute("Date");
                            attr.Value = tc.Date.ToString("yyyy-MM-dd");
                            tcNode.Attributes.Append(attr);

                            var acsNode = xdoc.CreateElement("Activities");
                            foreach (var ac in tc.Activities)
                            {
                                var acNode = xdoc.CreateElement("Activity");
                                acNode
                                  .AddAttribute("Code", ac.Code)
                                  .AddAttribute("Description", ac.Description)
                                  .AddAttribute("Time", ac.Time)
                                  .AddAttribute("IsAfterMidnight", ac.IsAfterMidnight.ToString());
                                acsNode.AppendChild(acNode);
                            }
                            tcNode.AppendChild(acsNode);

                            rootNode.AppendChild(tcNode);
                        }

                        xdoc.Save(sw);
                        break;

                    default:
                        throw new Exception("Unhandled data format encountered");
                }

                return sw.ToString();
            }
        }

        public void Import(string content, DataFormat format)
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
