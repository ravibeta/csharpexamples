using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SplunkLite.Net.Rest.Models
{
    public class Event
    {
        public String Raw { get; set; }
        public DateTime Time { get; set; }
        public String Source { get; set; }
        public String Host { get; set; }
        public String SourceType { get; set; }
        public String Index { get; set; }
    }
}