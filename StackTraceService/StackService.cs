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
            try
            {
                using (var proxy = new DebugClient())
                {



                }
            }
            catch (Exception e)
            {
                var str = e.Message;
                Console.WriteLine(str);
            }
            return null;
        }
    }
}
