using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    public class SearchProcessor
    {
        // TODO: only time inverted search to be enabled for Lite 
        // lexicon and postings based search later
        public bool Execute(SearchResults results, SearchResultsInfo info)
        {
            bool ret = false;
            foreach (var result in results.Results)
            {
                Console.WriteLine(string.Format("{0}", result.Raw));
            }
            ret = true;
            return ret;
        }
    }
}
