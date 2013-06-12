using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Debuggers.DbgEng;

namespace StackTraceService
{
    public class StackService : IStackService
    {
        public IEnumerable<string> GetStackTrace(string filename)
        {
            var ret = new List<string>();
            try
            {
                using (var proxy = new DebugClient())
                using (var control = new DebugControl(proxy))
                using (var symbol = new DebugSymbols(proxy))
                {
                    proxy.OpenDumpFile(filename);
                    var trace = control.GetStackTrace(10);
                    trace.ToList().ForEach(x => ret.Add(x.ToString()));                    
                }
            }
            catch (Exception e)
            {
                var str = e.Message;
                Console.WriteLine(str);
            }
            return ret;
        }
    }
}
