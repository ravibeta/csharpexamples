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
                // TODO: all we want to do here is store events in reverse time order.
                // Here we could implement a sliding window ring buffer to rectify out of order packets.

                // The sliding window has three regions
                 // Region 1 – Data received, sequenced and ready to flush to disk
                 // Region 2 – Data received but not yet sorted. (lookahead)
                 // Region 3 – unfilled buffer.

                // If we take a buffer with ten slots for events  where we assume the lookahead required to get all delayed and out of order events is less than ten, then we can safely implement this sliding window by waiting for the lookahead buffer to fill and moving the earliest packet into the sequenced region 1  

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
