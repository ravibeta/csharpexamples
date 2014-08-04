using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    public class CowPipelineData
    {
        public Stream Data {get; set;}
        public DateTime Time { get; set; }
        public String Source { get; set; }
        public String Host { get; set; }
        public String SourceType { get; set; }
        public String Index { get; set; }
    }
}
