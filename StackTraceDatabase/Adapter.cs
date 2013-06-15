using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceDatabase
{
    public class Adapter
    {
        public void SaveStackTrace(string filename, string stackTrace)
        {
            using (var context = new StackTraceDBEntities())
            {
                var entry = new StackTrace() { DumpFile = filename, StackTrace1 = stackTrace };
                context.AddToStackTraces(entry);
                context.SaveChanges();
            }
        }
    }
}
