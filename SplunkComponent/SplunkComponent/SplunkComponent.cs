using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Splunk;
using SplunkSDKHelper;

namespace SplunkComponent
{
    
    
    public interface ILogParserInputContext
    {
        void OpenInput(string from);
        int GetFieldCount();
        string GetFieldName(int index);
        int GetFieldType(int index);
        bool ReadRecord();
        object GetValue(int index);
        void CloseInput(bool abort);
    }


    [Guid("fb947990-aa8c-4de5-8ff3-32a59fb66a6c")]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class SplunkComponent // : ILogParserInputContext
    {
        public SplunkComponent()
        {
            // Load connection info for Splunk server in .splunkrc file.
            var cli = Command.Splunk("search");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(new string[] { "--search=\"search index=main\"" });
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine("Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            var service = Service.Connect(cli.Opts);
            var jobs = service.GetJobs();
            job = jobs.Create((string)cli.Opts["search"]);
            while (!job.IsDone)
            {
                System.Threading.Thread.Sleep(1000);
            }
            events = new List<Event>();
            fields = new List<LogField>();
            fields.Add(new LogField("_time", FieldType.Timestamp));
            fields.Add(new LogField("host", FieldType.String));
            fields.Add(new LogField("source", FieldType.String));
            fields.Add(new LogField("sourceype", FieldType.String));
            fields.Add(new LogField("raw", FieldType.String));

        }

        [System.Runtime.InteropServices.ComVisible(true)]
        public string GetAllResults()
        {
            var outArgs = new JobResultsArgs
            {
                OutputMode = JobResultsArgs.OutputModeEnum.Xml,

                // Return all entries.
                Count = 0
            };

            using (var stream = job.Results(outArgs))
            {
                var setting = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                };

                using (var rr = new ResultsReaderXml(stream))
                {
                    foreach (var @event in rr)
                    {
                        events.Add(@event);
                    }
                }

                using (var rr = XmlReader.Create(stream, setting))
                {
                    return rr.ReadOuterXml();
                }
            }
            return null;
        }

        private Job job { get; set; }
        private string doc { get; set; }
        private List<LogField> fields { get; set; }
        private List<Event> events { get; set; }
        private int eventIndex { get; set; }
        #region LogField Class
        /// <summary>InputFormat.FieldType Enumeration</summary>
        private enum FieldType
        {
            /// <summary>VT_I8</summary>
            Integer = 1,
            /// <summary>VT_R8</summary>
            Real = 2,
            
            /// <summary>VT_BSTR</summary>
            String = 3,
            /// <summary>VT_DATE or VT_I8 (UTC)</summary>
            Timestamp = 4
        }
        private class LogField
        {
            string fieldName;
            FieldType fieldType;

            public LogField(string FieldName, FieldType FieldType)
            {
                fieldName = FieldName;
                fieldType = FieldType;
            }

            public string FieldName
            {
                get { return fieldName; }
                set { fieldName = value; }
            }

            public FieldType FieldType
            {
                get { return fieldType; }
                set { fieldType = value; }
            }
        }
        #endregion

[System.Runtime.InteropServices.ComVisible(true)]
public void OpenInput(string from)
{
    doc = GetAllResults();
    eventIndex = 0;
}

[System.Runtime.InteropServices.ComVisible(true)]
public int GetFieldCount()
{
    return 5;
}

[System.Runtime.InteropServices.ComVisible(true)]
public string GetFieldName(int index)
{
    LogField logfield = (LogField)fields[index];
    return logfield.FieldName;
}

[System.Runtime.InteropServices.ComVisible(true)]
public int GetFieldType(int index)
{
    LogField logfield = (LogField)fields[index];
    return (int)logfield.FieldType;
}

[System.Runtime.InteropServices.ComVisible(true)]
public bool ReadRecord()
{
    eventIndex++;
    if (eventIndex > events.Count) return false;
    return true;
}

[System.Runtime.InteropServices.ComVisible(true)]
public object GetValue(int index)
{
    if (index < 0 || index > fields.Count) return null;
    var e = events[eventIndex];
    LogField lf = (LogField)fields[index];

    if (String.Compare(lf.FieldName, "_time") == 0 ||
        String.Compare(lf.FieldName, "host") == 0 ||
        String.Compare(lf.FieldName, "source") == 0 ||
        String.Compare(lf.FieldName, "sourcetype") == 0 ||
        String.Compare(lf.FieldName, "_raw") == 0)
    {
        return e[lf.FieldName.ToLower()].ToString();
    }
    return null;

}

[System.Runtime.InteropServices.ComVisible(true)]
public void CloseInput(bool abort)
{
    eventIndex = 0;
    events.Clear();
}
}
}
