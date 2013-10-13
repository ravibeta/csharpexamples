using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SourceIndexServer.Models
{
    public class SearchResponse
    {
        public List<Uri> Match { get; set; }
    }
}