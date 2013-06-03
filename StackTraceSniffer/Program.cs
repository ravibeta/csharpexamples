using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StackTraceSniffer
{
    class Program
    {
        static void Main(string[] args)
        {
            var stacktrace = ReadDumpFile(args.First());
            Console.WriteLine(stacktrace);
        }

        private static string ReadDumpFile(string dumpFileName)
        {
            string ret = string.Empty;
            using (var fs = new FileStream(dumpFileName,FileMode.Open))
            {
                var offset = 0;
                var size = 1024;
                var bytes = new Byte[1024];
                var s = fs.Read(bytes, 0, size);
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(BitConverter.ToChar(bytes, offset + i));
                }
            }
            return ret;
        }
    }
}
