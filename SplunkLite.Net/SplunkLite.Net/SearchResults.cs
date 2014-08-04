using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
        public Dictionary<string, string> KeyValues { get; set; }
        
    }
    public class SearchResult
    {
        public string Raw { get; set; }
    }
    public class SearchResultsInfo
    {
        Func<SearchResults, SearchResults> selector { get; set; }
    }
}
