using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.Debuggers.DbgEng;
using System.ComponentModel;

namespace StackTracePS
{
    [Cmdlet(VerbsCommon.Get, "StackTrace")]
    public class GetStackTraceCommand : Cmdlet
    {
        private List<string> frames;
        private string dumpName;

        protected override void BeginProcessing()
        {
            try
            {
                if (String.IsNullOrEmpty(this.dumpName)) return;
                var frames = GetStackTrace(this.dumpName);
                var output = string.Empty;
                frames.ToList().ForEach(x => output += x + "\r\n");
                WriteObject(frames);
            }
            catch (Exception e)
            {
                var str = e.Message;
                Console.WriteLine(str);
            }
        }

        protected override void ProcessRecord()
        {

        }
        
        protected override void EndProcessing()
        {
        }
        
            /// <summary>
            /// Gets or sets the list of process names on which 
            /// the Get-Proc cmdlet will work.
            /// </summary>
            [Parameter(Position = 0)]
            [ValidateNotNullOrEmpty]
            public string DumpName
            {
              get { return this.dumpName; }
              set { this.dumpName = value; }
            }

        private IEnumerable<string> GetStackTrace(string filename)
        {
            frames = new List<string>();
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

                        var trace = control.GetStackTrace(10);
                        frames.Clear();
                        control.OutputStackTrace(OutputControl.ToAllClients, trace.ToArray(), StackTraceOutput.Default);

                    }
                }
            }
            return frames;
        }

        void proxy_DebugOutput(object sender, DebugOutputEventArgs e)
        {
            var str = e.Output;
            str.Split(new char[] { '\n' }).ToList().ForEach(x => frames.Add(x));
        }
    }
      #region PowerShell snap-in
  /// <summary>
  /// Create the Windows PowerShell snap-in for this sample.
  /// </summary>
  [RunInstaller(true)]
  public class GetStackTracePSSnapIn01 : PSSnapIn
  {
    /// <summary>
    /// Initializes a new instance of the GetProcPSSnapIn01 class.
    /// </summary>
    public GetStackTracePSSnapIn01()
           : base()
    {
    }
    
    /// <summary>
    /// Get a name for the snap-in. This name is used to register
    /// the snap-in.
    /// </summary>
    public override string Name
    {
      get
      {
        return "GetStackTracePSSnapIn01";
      }
    }
    
    /// <summary>
    /// Get the name of the vendor of the snap-in.
    /// </summary>
    public override string Vendor
    {
      get
      {
        return "Microsoft";
      }
    }
    
    /// <summary>
    /// Get the resource information for vendor. This is a string of format: 
    /// resourceBaseName,resourceName. 
    /// </summary>
    public override string VendorResource
    {
      get
      {
        return "GetStackTracePSSnapIn01,Microsoft";
      }
    }
    
    /// <summary>
    /// Get a description of the snap-in.
    /// </summary>
    public override string Description
    {
      get
      {
        return "This is a PowerShell snap-in that includes the get-stacktrace cmdlet.";
      }
    }
  }
  #endregion PowerShell snap-in
}
