using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Xml;
using TimecardsCore.Events;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Models;

namespace TimecardsCore.Logic
{
    /// <summary>
    /// This class is used for importing and exporting timecard data; for the import operation, it raises an
    /// event to report the progress of the import
    /// </summary>
    public class BulkLogic
    {
        /// <summary>
        /// Reports the progress of the import
        /// </summary>
        public event EventHandler<ProgressUpdateEventArgs>? ProgressUpdated;

        private readonly IFactory _factory;

        #region Constructor

        public BulkLogic(IFactory factory)
        {
            _factory = factory;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Exports timecard data in given date range to a string in the given format
        /// </summary>
        /// <param name="startDate">Low end of date range</param>
        /// <param name="endDate">High end of date range</param>
        /// <param name="format">Enum value corresponding to CSV, TSV, JSON, or XML format</param>
        /// <returns>String of data encoded in given format</returns>
        public string Export(DateTime? startDate, DateTime? endDate, DataFormat format)
        {
            var repo = _factory.Resolve<IRepository>();

            // retrieve the timecards to be exported
            var tcList = repo.GetTimecards(startDate, endDate);

            using var sw = new StringWriter();

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
                    var jsonSettings = new JsonSerializerOptions
                    {
                    };
                    sw.WriteLine(JsonSerializer.Serialize(tcList, jsonSettings));
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

        public static string ExportReport(List<ReportItem> reportList, DataFormat format)
        {
            using var sw = new StringWriter();
            switch (format)
            {
                case DataFormat.CSV:
                    sw.WriteLine("Code,Earliest,Latest,TotalMinutes");
                    foreach (var ri in reportList)
                    {
                        sw.WriteLine($"\"{ri.Code}\",\"{ri.EarliestDate:yyyy-MM-dd}\",\"{ri.LatestDate:yyyy-MM-dd}\",{ri.TotalMinutes}");
                    }
                    break;

                case DataFormat.TSV:
                    sw.WriteLine("Code\tEarliest\tLatest\tTotalMinutes");
                    foreach (var ri in reportList)
                    {
                        sw.WriteLine($"{ri.Code}\t{ri.EarliestDate:yyyy-MM-dd}\t{ri.LatestDate:yyyy-MM-dd}\t{ri.TotalMinutes}");
                    }
                    break;

                case DataFormat.JSON:
                    var jsonSettings = new JsonSerializerOptions
                    {
                    };
                    sw.WriteLine(JsonSerializer.Serialize(reportList, jsonSettings));
                    break;

                case DataFormat.XML:
                    var xdoc = new XmlDocument();

                    var rootNode = xdoc.CreateElement("Report");
                    xdoc.AppendChild(rootNode);

                    foreach (var ri in reportList)
                    {
                        var itemNode = xdoc.CreateElement("ReportItem");
                        itemNode
                            .AddAttribute("Code", ri.Code ?? "")
                            .AddAttribute("EarliestDate", ri.EarliestDate.ToString("yyyy-MM-dd"))
                            .AddAttribute("LatestDate", ri.LatestDate.ToString("yyyy-MM-dd"))
                            .AddAttribute("TotalMinutes", ri.TotalMinutes.ToString());

                        rootNode.AppendChild(itemNode);
                    }

                    xdoc.Save(sw);
                    break;

                default:
                    throw new Exception("Unhandled data format encountered");
            }

            return sw.ToString();
        }

        /// <summary>
        /// Imports timecard data from the given string; raises ProgressUpdated event as timecards are imported
        /// </summary>
        /// <param name="content">The data to be imported, in given format</param>
        /// <param name="format">Enum value corresponding to CSV, TSV, JSON, or XML format</param>
        /// <returns>String containing error message (if any) encountered during import</returns>
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
                    Timecard? tc = null;

                    var lastDateString = "zzzz";
                    for (var i = 1; i < lines.Length; ++i)
                    {
                        var eventArgs = new ProgressUpdateEventArgs(i + 1, lines.Length);
                        OnProgressUpdated(eventArgs);
                        if (eventArgs.Cancel)
                        {
                            report.AppendLine("Import was canceled by the user");
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(lines[i]))
                            continue;

                        var tokens = Split(lines[i], separator);

                        if (tokens[columnMap["date"]] != lastDateString)
                        {
                            lastDateString = tokens[columnMap["date"]];

                            if (tc != null)
                            {
                                repo.SaveTimecard(tc);
                            }

                            tc = new Timecard();
                            tc.Activities.DataImportMode = true;

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

                        var activity = new TimecardsCore.Models.Activity
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
                        else
                        {
                            activity.IsAfterMidnight = false;
                        }

                        tc?.Activities.Add(activity);
                    }

                    if (tc != null && tc.IsDirty)
                    {
                        repo.SaveTimecard(tc);
                    }

                    break;

                case DataFormat.JSON:
                    List<Timecard>? tcList;

                    try
                    {
                        tcList = JsonSerializer.Deserialize<List<Timecard>>(content);
                    }
                    catch (Exception ex)
                    {
                        report.AppendLine($"Error occurred while parsing JSON data: {ex.Message}");
                        break;
                    }

                    if (tcList != null)
                    {
                        for (var i = 0; i < tcList.Count; ++i)
                        {
                            var eventArgs = new ProgressUpdateEventArgs(i + 1, tcList.Count);
                            OnProgressUpdated(eventArgs);
                            if (eventArgs.Cancel)
                            {
                                report.AppendLine("Import was canceled by the user");
                                break;
                            }

                            repo.SaveTimecard(tcList[i]);
                        }
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
                            var eventArgs = new ProgressUpdateEventArgs(i + 1, root.ChildNodes.Count);
                            OnProgressUpdated(eventArgs);
                            if (eventArgs.Cancel)
                            {
                                report.AppendLine("Import was canceled by the user");
                                break;
                            }

                            var tcNode = root.ChildNodes[i];
                            if (tcNode?.Name != "Timecard")
                            {
                                report.AppendLine("Expected parent node in XML data not named \"Timecard\"");
                                break;
                            }

                            var tcAttributes = tcNode.Attributes;

                            var tcAttrDate = tcAttributes?["Date"];
                            if (tcAttrDate == null)
                            {
                                report.AppendLine($"Timecard node {i} missing Date attribute");
                                break;
                            }

                            tc = new Timecard();
                            tc.Activities.DataImportMode = true;

                            if (DateTime.TryParse(tcAttributes?["Date"]?.Value, out DateTime newDate))
                            {
                                tc.Date = newDate;
                            }
                            else
                            {
                                report.AppendLine(
                                    $"Timecard node {i} Date attribute value \"{tcAttributes?["Date"]?.Value}\" could not be parsed");
                                break;
                            }

                            if (tcNode.HasChildNodes)
                            {
                                var acCollNode = tcNode.ChildNodes[0];
                                if (acCollNode?.HasChildNodes ?? false)
                                {
                                    for (var j = 0; j < acCollNode.ChildNodes.Count; ++j)
                                    {
                                        var acNode = acCollNode.ChildNodes[j];

                                        var activity = new TimecardsCore.Models.Activity
                                        {
                                            Code = $"{acNode?.Attributes?["Code"]?.Value}",
                                            Description = $"{acNode?.Attributes?["Description"]?.Value}",
                                            Time = $"{acNode?.Attributes?["Time"]?.Value}"
                                        };

                                        if (Boolean.TryParse($"{acNode?.Attributes?["IsAfterMidnight"]?.Value}",
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

        private static Dictionary<string, int> ParseColumnMap(string line, char separator, List<string> allowedColumnNames)
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

        private static string[] Split(string value, char separator)
        {
            var result = new List<string>();
            var inQuotes = false;
            int lastIndex = -1;

            for (var i = 0; i < value.Length; ++i)
            {
                if (value[i] == '"')
                {
                    if (i - 1 == lastIndex)
                    {
                        inQuotes = true;
                        continue;
                    }

                    inQuotes = false;
                }

                if (value[i] == separator)
                {
                    if (inQuotes)
                        continue;

                    result.Add(value.Substring(lastIndex + 1, i - lastIndex - 1));
                    lastIndex = i;
                }
            }

            result.Add(value[(lastIndex + 1)..]);

            return result.ToArray();
        }

        private static string StripQuotes(string value)
        {
            if (value.Length > 1 && value[0] == '"' && value[^1] == '"')
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

        private void OnProgressUpdated(ProgressUpdateEventArgs e)
        {
            ProgressUpdated?.Invoke(this, e);
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
