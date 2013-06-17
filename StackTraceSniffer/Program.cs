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

            return frames;
        }

        static void proxy_DebugOutput(object sender, DebugOutputEventArgs e)
        {
            var str = e.Output;
            str.Split(new char[] { '\n' }).ToList().ForEach(x => frames.Add(x));
        }
    }
}
