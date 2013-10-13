using Lucene.Net.Index;
using Lucene.Net.Store;
using SourceIndexServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SourceIndexServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
            : base()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchRequest query)
        {
            if (query == null || String.IsNullOrEmpty(query.Term)) return View("Search");
            var response = new List<Match>() { };
         
            // we use an index we built earlier. Code commented out below
            var indexAt = SimpleFSDirectory.Open(new DirectoryInfo(@"C:\Code\Index2"));
            using (var reader = IndexReader.Open(indexAt, true))
            {
                var pos = reader.TermPositions(new Term("contents", query.Term.ToLower()));
                while (pos.Next())
                {
                    var item = reader.Document(pos.Doc).GetValues("title").FirstOrDefault();
                    if (item != null)
                    {
                        var match = new Match() { item = new Uri(item) };
                        response.Add(match);
                    }
                }
            }
            return View("Matches",response);
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Lucene.Net;
//using Lucene.Net.Analysis;
//using Lucene.Net.Documents;
//using Lucene.Net.Index;
//using Lucene.Net.QueryParsers;
//using Lucene.Net.Store;
//using Lucene.Net.Analysis.Standard;

//namespace SourceSearch
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            if (args.Count() != 1)
//            {
//                Console.WriteLine("Usage: SourceSearch <term>");
//                return;
//            }

//            var indexAt = SimpleFSDirectory.Open(new DirectoryInfo(@"C:\Code\Index2"));
//            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
//            using (var indexer = new IndexWriter(
//                indexAt,
//                analyzer, true,
//                IndexWriter.MaxFieldLength.UNLIMITED))
//            {

//                var src = new DirectoryInfo(@"C:\code\API");
//                var source = new SimpleFSDirectory(src);

//                src.EnumerateFiles("*.cs", SearchOption.AllDirectories).ToList()
//                    .ForEach(x =>
//                        {
//                            using (var reader = File.OpenText(x.FullName))
//                            {
//                                var doc = new Document();
//                                TeeSinkTokenFilter tfilter = new TeeSinkTokenFilter(new WhitespaceTokenizer(reader));
//                                TeeSinkTokenFilter.SinkTokenStream sink = tfilter.NewSinkTokenStream();
//                                TokenStream final = new LowerCaseFilter(tfilter);
//                                doc.Add(new Field("contents", final));
//                                doc.Add(new Field("title", x.FullName, Field.Store.YES, Field.Index.ANALYZED));
//                                indexer.AddDocument(doc);
//                            }
//                        });

//                indexer.Optimize();
//                Console.WriteLine("Total number of files indexed : " + indexer.MaxDoc());
//            }

//            using (var reader = IndexReader.Open(indexAt, true))
//            {
//                var pos = reader.TermPositions(new Term("contents", args.First().ToLower()));
//                while (pos.Next())
//                {
//                    Console.WriteLine("Match in document " + reader.Document(pos.Doc).GetValues("title").FirstOrDefault());
//                }
//            }
//        }
//    }
//}
