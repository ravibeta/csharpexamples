using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splunk;
using SplunkSDKHelper;
using System.Xml;

namespace SplunkComponent
{

    [System.Runtime.InteropServices.ComVisible(false)]
    public class SplunkComponent
    {
        public SplunkComponent()
        {
            // Load connection info for Splunk server in .splunkrc file.
            var cli = Command.Splunk("search");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(new string[] {"--search=\"index=main\""});
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
        }

        [System.Runtime.InteropServices.ComVisible(false)]
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

                using (var rr = XmlReader.Create(stream, setting))
                {
                    return rr.ReadOuterXml();
                }
            }
        }

        private Job job { get; set; }
    }
}
