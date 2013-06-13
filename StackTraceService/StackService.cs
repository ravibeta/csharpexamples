using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Debuggers.DbgEng;

namespace StackTraceService
{
    public class StackService : IStackService
    {
        private List<string> frames;
        public IEnumerable<string> GetStackTrace(string filename)
        {
            frames = new List<string>();
            try
            {
                using (var proxy = new DebugClient())
                {
                    var client = proxy.CreateClient();
                    client.OpenDumpFile(filename);
                    using (var symbol = new DebugSymbols(client))
                    using (var control = new DebugControl(client))
                    {
                        control.WaitForEvent();
                        {
                            proxy.DebugOutput += new EventHandler<DebugOutputEventArgs>(proxy_DebugOutput);
                            //proxy.ExceptionHit += new EventHandler<ExceptionEventArgs>(proxy_ExceptionHit);

                            var trace = control.GetStackTrace(10);
                            control.OutputStackTrace(OutputControl.ToAllClients, trace.ToArray(), StackTraceOutput.Default);
                            
                        }
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

        void proxy_ExceptionHit(object sender, ExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        void proxy_DebugOutput(object sender, DebugOutputEventArgs e)
        {
            var str = e.Output;
            str.Split(new char[] {'\n'}).ToList().ForEach(x => frames.Add(x));
        }
    }
}
