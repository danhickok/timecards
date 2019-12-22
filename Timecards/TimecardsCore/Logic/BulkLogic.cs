using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
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
                    case DataFormat.CSV:
                        sw.WriteLine("Date,Code,Description,Time,IsAfterMidnight");
                        foreach (var tc in tcList)
                        {
                            foreach (var ac in tc.Activities)
                            {
                                sw.WriteLine($"\"{tc.Date:yyyy-MM-dd}\",\"{ac.Code}\",\"{ac.Description}\",\"{ac.Time}\",{ac.IsAfterMidnight}");
                            }
                        }
                        break;

                    case DataFormat.TSV:
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

        public string Import(string content, DataFormat format)
        {
            string[] lines;
            Dictionary<string, int> columnMap;

            var allowedColumnNames = new List<string> { "date", "code", "description", "time", "isaftermidnight" };
            var report = new StringBuilder();
            var repo = _factory.Resolve<IRepository>();

            switch (format)
            {
                case DataFormat.CSV:
                    lines = content.Replace("\r", string.Empty).Split('\n');
                    if (lines.Length < 1)
                    {
                        report.AppendLine("Content contains no lines of text");
                        break;
                    }

                    columnMap = ParseColumnMap(lines[0], ',', allowedColumnNames);

                    var missing = false;
                    for (var i = 0; i < allowedColumnNames.Count - 1; ++i)
                    {
                        if (!columnMap.ContainsKey(allowedColumnNames[i]))
                        {
                            report.AppendLine($"Content missing column name '{allowedColumnNames[i]}' on first line");
                            missing = true;
                        }
                    }
                    if (missing)
                    {
                        break;
                    }

                    //TODO: sort remaining lines
                    //TODO: parse and store, raising event for progress
                    break;

                case DataFormat.TSV:
                    lines = content.Replace("\r", string.Empty).Split('\n');
                    if (lines.Length < 1)
                    {
                        report.AppendLine("Content contains no lines of text");
                        break;
                    }

                    columnMap = ParseColumnMap(lines[0], ',', allowedColumnNames);

                    //TODO: finish
                    break;

                case DataFormat.JSON:
                    //TODO: finish
                    break;

                case DataFormat.XML:
                    //TODO: finish
                    break;

                default:
                    throw new Exception("Unhandled data format encountered");
            }

            return report.ToString();
        }

        public enum DataFormat
        {
            [Description("Comma-delimited text")]
            CSV,

            [Description("Tab-delimited text")]
            TSV,

            [Description("JSON")]
            JSON,

            [Description("XML")]
            XML
        }

        private Dictionary<string, int> ParseColumnMap(string line, char separator, List<string> allowedColumnNames)
        {
            var map = new Dictionary<string, int>();

            var tokens = line.ToLower().Split(separator);
            for (var i = 0; i < tokens.Length; ++i)
            {
                if (allowedColumnNames.Contains(tokens[i]))
                {
                    map[tokens[i]] = i;
                }
            }

            return map;
        }
    }
}
