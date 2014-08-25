using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            // sample end to end invocation
            var input = new FileInputProcessor();
            var data = input.CreateInputPipelineData();
            if (input.Execute(data))
            {
                var db = Directory.EnumerateFiles(Environment.CurrentDirectory).FirstOrDefault(x => x.Contains(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()));
                if (db == null) return;
                using (var reader = new StreamReader(db))
                {
                    var results = new SearchResults();
                    results.Results = new List<SearchResult>();
                    reader.ReadToEnd().Split(new char[] { '\n' }).ToList().ForEach(x => { var result = new SearchResult(); result.Raw = x; results.Results.Add(result); });
                    var info = new SearchResultsInfo();
                    var search = new SearchProcessor();
                    search.Execute(results, info);
                }
            }
        }
    }
}
