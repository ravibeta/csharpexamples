using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    public class IndexProcessor
    {
        public bool write(CowPipelineData data, DateTime indexTime)
        {
            bool ret = false;
            using (var reader = new StreamReader(data.Data))
            {
                // TODO: change bucket persist  and lookup
                var bucketId = Guid.NewGuid().ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                using (var writer = new StreamWriter(bucketId, true, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // make events
                        // write to hot bucket in Db
                        writer.WriteLine(string.Format("host = {0}, source = {1}, sourcetype = {2}, index = {3}, _time = {4}, _raw = {5}",
                            data.Host,
                            data.Source,
                            data.SourceType,
                            data.Index,
                            data.Time,
                            line));
                    }
                }
            }
            ret = true;
            return ret;
        }
    }
}
