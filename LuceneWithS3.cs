using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;

namespace SourceSearch
{
    class Program
    {
        private const string bucketName = "ravi-rajamani-shared";
        private const string keyName1 = "searchIndex";
        private const string filePath = @"\Code\Index2";
        private const string sourcePath = @"\code\API";
        private static string storageConnectionString = Environment.GetEnvironmentVariable("storageconnectionstring");
       

        static void Main(string[] args)
        {
            if (args.Count() != 1)
            {
                Console.WriteLine("Usage: SourceSearch <term>");
                return;
            }
            try {
                CloudStorageAccount storageAccount = null;
                CloudBlobContainer cloudBlobContainer = null;
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                cloudBlobContainer = cloudBlobClient.GetContainerReference(bucketName + Guid.NewGuid().ToString());
                cloudBlobContainer.Create();
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                cloudBlobContainer.SetPermissions(permissions);
                var indexAt = SimpleFSDirectory.Open(new DirectoryInfo(@"C:\Code\Index2"));
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                using (var indexer = new IndexWriter(
                    indexAt,
                    analyzer, true,
                    IndexWriter.MaxFieldLength.UNLIMITED))
                {

                    var src = new DirectoryInfo(sourcePath);
                    var source = new SimpleFSDirectory(src);

                    src.EnumerateFiles("*.cs", SearchOption.AllDirectories).ToList()
                        .ForEach(x =>
                            {
                                using (var reader = File.OpenText(x.FullName))
                                {
                                    var doc = new Document();
                                    TeeSinkTokenFilter tfilter = new TeeSinkTokenFilter(new WhitespaceTokenizer(reader));
                                    TeeSinkTokenFilter.SinkTokenStream sink = tfilter.NewSinkTokenStream();
                                    TokenStream final = new LowerCaseFilter(tfilter);
                                    doc.Add(new Field("contents", final));
                                    doc.Add(new Field("title", x.FullName, Field.Store.YES, Field.Index.ANALYZED));
                                    indexer.AddDocument(doc);
                                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(x.Name);
                                    MemoryStream stream = new MemoryStream();
                                    var writer = new StreamWriter(stream);
                                    writer.Write(doc.ToString());
                                    stream.Position = 0;
                                    cloudBlockBlob.UploadFromStream(stream);
                                }
                            });

                    indexer.Optimize();
                    Console.WriteLine("Total number of files indexed : " + indexer.MaxDoc());
                }

                using (var reader = IndexReader.Open(indexAt, true))
                {
                    var pos = reader.TermPositions(new Term("contents", args.First().ToLower()));
                    while (pos.Next())
                    {
                        Console.WriteLine("Match in document " + reader.Document(pos.Doc).GetValues("title").FirstOrDefault());
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
