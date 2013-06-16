using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Debuggers.DbgEng;

namespace StackTraceSniffer
{
    class Program
    {
        private static List<string> frames;

        static void Main(string[] args)
        {
            try
            {
                var frames = GetStackTrace(args.First());
                frames.ToList().ForEach(x => Console.WriteLine(x));
            }
            catch (Exception e)
            {
                var str = e.Message;
                Console.WriteLine(str);
            }
        }

        private static IEnumerable<string> GetStackTrace(string filename)
        {
            frames = new List<string>();
            try
            {
                using (var proxy = new DebugClient())
                {
                    using (var client = proxy.CreateClient())
                    {
                        client.OpenDumpFile(filename);
                        using (var symbol = new DebugSymbols(client))
                        using (var control = new DebugControl(client))
                        {
                            control.WaitForEvent();
                            {
                                proxy.DebugOutput += new EventHandler<DebugOutputEventArgs>(proxy_DebugOutput);

                                var trace = control.GetStackTrace(10);
                                frames.Clear();
                                control.OutputStackTrace(OutputControl.ToAllClients, trace.ToArray(), StackTraceOutput.Default);

                            }
                        }

                        client.FlushCallbacks();
                        client.EndSession(EndSessionMode.ActiveTerminate);
                    }
                }
            }
            catch (Exception e)
            {
                var str = e.Message;
                Console.WriteLine(str);
            }
            return frames;

        }

        static void proxy_DebugOutput(object sender, DebugOutputEventArgs e)
        {
            var str = e.Output;
            str.Split(new char[] { '\n' }).ToList().ForEach(x => frames.Add(x));
        }

        private static string ReadDumpFile(string dumpFileName)
        {
            string ret = string.Empty;
            using (var fs = new FileStream(dumpFileName,FileMode.Open))
            {
                var size = 4096;
                var bytes = new Byte[4096]; // The start signature may not be "DUMP"
                var s = fs.Read(bytes, 0, size);
                var numStreams = BitConverter.ToUInt32(bytes, 8);
                var rvaStream = BitConverter.ToUInt32(bytes, 12);
                var exceptionStreamOffset = rvaStream + 12; // skip first directory;
                
                var rvaExceptionStream = BitConverter.ToUInt32(bytes, (int)exceptionStreamOffset + (int)8); 
                var contextLocationOffset = rvaExceptionStream + 32 + 128; // based on MINIDUMP_EXCEPTION
                var contextSize = BitConverter.ToUInt32(bytes, (int)contextLocationOffset); // MINIDUMP_LOCATION_DESCRIPTOR
                var contextOffset = BitConverter.ToUInt32(bytes, (int)contextLocationOffset + 4); // RVA

                // read the stack pointer from context 
                var stackPointerOffset = BitConverter.ToUInt32(bytes, (int)contextOffset + 0xc4); // _CEDUMP_ELEMENT_LIST

                for (int i = 0; i < numStreams; i++)
                {
                    // find memory stream _MINIDUMP_STREAM_TYPE  == 5
                    // dump stack pointer
                    

                }

                
                
            }
            return ret;
        }
    }
}
