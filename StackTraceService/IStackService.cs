using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceService
{
    public interface IStackService
    {
        IEnumerable<string> GetStackTrace(string filename);
    }
}
