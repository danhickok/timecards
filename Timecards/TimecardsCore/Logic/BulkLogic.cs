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
using TimecardsCore.Models;

namespace TimecardsCore.Logic
{
    public class BulkLogic
    {
        private readonly IFactory _factory;

        #region Constructor

        public BulkLogic(IFactory factory)
        {
            _factory = factory;
        }

        #endregion

        #region Public methods

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
                case DataFormat.TSV:
                    var separator = (format == DataFormat.CSV ? ',' : '\t');

                    lines = content.Replace("\r", string.Empty).Split('\n');
                    if (lines.Length < 1)
                    {
                        report.AppendLine("Content contains no lines of text");
                        break;
                    }

                    columnMap = ParseColumnMap(lines[0], separator, allowedColumnNames);

                    var someFieldsAreMissing = false;
                    for (var i = 0; i < allowedColumnNames.Count - 1; ++i)
                    {
                        if (!columnMap.ContainsKey(allowedColumnNames[i]))
                        {
                            report.AppendLine($"Content missing column name '{allowedColumnNames[i]}' on first line");
                            someFieldsAreMissing = true;
                        }
                    }
                    if (someFieldsAreMissing)
                    {
                        break;
                    }

                    // parse and store, raising event for progress
                    Timecard tc = null;

                    var lastDateString = "zzzz";
                    for (var i = 1; i < lines.Length; ++i)
                    {
                        //TODO: raise event

                        if (string.IsNullOrWhiteSpace(lines[i]))
                            continue;

                        var tokens = lines[i].Split(separator);

                        if (tokens[columnMap["date"]] != lastDateString)
                        {
                            lastDateString = tokens[columnMap["date"]];

                            if (tc != null)
                            {
                                repo.SaveTimecard(tc);
                            }

                            tc = new Timecard();
                            if (DateTime.TryParse(StripQuotes(lastDateString), out DateTime newDate))
                            {
                                tc.Date = newDate;
                            }
                            else
                            {
                                report.AppendLine($"Could not parse value {lastDateString} as date on line {i + 1}");
                                break;
                            }
                        }

                        var activity = new Activity
                        {
                            Code = StripQuotes(tokens[columnMap["code"]]),
                            Description = StripQuotes(tokens[columnMap["description"]]),
                            Time = StripQuotes(tokens[columnMap["time"]])
                        };

                        if (columnMap.ContainsKey("isaftermidnight"))
                        {
                            if (Boolean.TryParse(StripQuotes(tokens[columnMap["isaftermidnight"]]), out bool newIsAfterMidnight))
                            {
                                activity.IsAfterMidnight = newIsAfterMidnight;
                            }
                            else
                            {
                                report.AppendLine(
                                    $"Could not parse value {tokens[columnMap["isaftermidnight"]]} as boolean on line {i + 1}");
                                break;
                            }
                        }

                        tc.Activities.Add(activity);
                    }

                    if (tc != null && tc.IsDirty)
                    {
                        repo.SaveTimecard(tc);
                    }

                    break;

                case DataFormat.JSON:
                    List<Timecard> tcList = null;

                    try
                    {
                        tcList = JsonConvert.DeserializeObject<List<Timecard>>(content);
                    }
                    catch (Exception ex)
                    {
                        report.AppendLine($"Error occurred while parsing JSON data: {ex.Message}");
                        break;
                    }

                    foreach (var timecard in tcList)
                    {
                        //TODO: raise event

                        repo.SaveTimecard(timecard);
                    }
                    break;

                case DataFormat.XML:
                    var xdoc = new XmlDocument();
                    try
                    {
                        xdoc.LoadXml(content);
                    }
                    catch (Exception ex)
                    {
                        report.AppendLine($"Error occurred while parsing XML data: {ex.Message}");
                        break;
                    }

                    var root = xdoc.SelectSingleNode("/Timecards");
                    if (root?.Name != "Timecards")
                    {
                        report.AppendLine("Root node in XML data not named \"Timecards\"");
                        break;
                    }

                    if (root.HasChildNodes)
                    {
                        for (var i = 0; i < root.ChildNodes.Count; i++)
                        {
                            //TODO: raise event

                            var tcNode = root.ChildNodes[i];
                            if (tcNode.Name != "Timecard")
                            {
                                report.AppendLine("Expected parent node in XML data not named \"Timecard\"");
                                break;
                            }

                            var tcAttributes = tcNode.Attributes;

                            var tcAttrDate = tcAttributes["Date"];
                            if (tcAttrDate == null)
                            {
                                report.AppendLine($"Timecard node {i} missing Date attribute");
                                break;
                            }

                            tc = new Timecard();

                            if (DateTime.TryParse(tcAttributes["Date"].Value, out DateTime newDate))
                            {
                                tc.Date = newDate;
                            }
                            else
                            {
                                report.AppendLine(
                                    $"Timecard node {i} Date attribute value \"{tcAttributes["Date"].Value}\" could not be parsed");
                                break;
                            }

                            if (tcNode.HasChildNodes)
                            {
                                var acCollNode = tcNode.ChildNodes[0];
                                if (acCollNode.HasChildNodes)
                                {
                                    for (var j = 0; j < acCollNode.ChildNodes.Count; ++j)
                                    {
                                        var acNode = acCollNode.ChildNodes[j];

                                        var activity = new Activity();

                                        activity.Code = $"{acNode.Attributes["Code"]?.Value}";
                                        activity.Description = $"{acNode.Attributes["Description"]?.Value}";
                                        activity.Time = $"{acNode.Attributes["Time"]?.Value}";

                                        if (Boolean.TryParse($"{ acNode.Attributes["IsAfterMidnight"]?.Value}",
                                            out bool newIsAfterMidnight))
                                        {
                                            activity.IsAfterMidnight = newIsAfterMidnight;
                                        }

                                        tc.Activities.Add(activity);
                                    }
                                }
                            }

                            repo.SaveTimecard(tc);
                        }
                    }
                    break;

                default:
                    throw new Exception("Unhandled data format encountered");
            }

            return report.ToString();
        }

        #endregion

        #region Private methods

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

        private string StripQuotes(string value)
        {
            if (value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"')
            {
                var sb = new StringBuilder(value);
                sb.Remove(0, 1);
                sb.Remove(sb.Length - 1, 1);
                sb.Replace("\\\"", "\"");
                return sb.ToString();
            }
            else
            {
                return value;
            }
        }

        #endregion

        #region Nested definitions

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

        #endregion
    }
}
