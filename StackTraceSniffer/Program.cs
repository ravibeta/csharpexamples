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
                var size = 1024;
                var bytes = new Byte[1024]; // The start signature may not be "DUMP"
                var s = fs.Read(bytes, 0, size);
                var numStreams = BitConverter.ToUInt32(bytes, 8);
                var rvaStream = BitConverter.ToUInt32(bytes, 12);
                var exceptionStreamOffset = rvaStream + 12; // skip first directory;
                
                var rvaExceptionStream = BitConverter.ToUInt32(bytes, (int)exceptionStreamOffset + 8); 
                var contextLocationOffset = rvaExceptionStream + 32 + 128; // based on MINIDUMP_EXCEPTION
                var contextSize = BitConverter.ToUInt32(bytes, (int)contextLocationOffset); // MINIDUMP_LOCATION_DESCRIPTOR
                var contextOffset = BitConverter.ToUInt32(bytes, (int)contextLocationOffset + 4); // RVA

                // read the stack pointer from context 
                
            }
            return ret;
        }
    }
}
