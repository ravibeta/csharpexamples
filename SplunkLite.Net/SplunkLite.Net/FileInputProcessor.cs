using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SplunkLite.Net
{
    public class FileInputProcessor
    {
        public virtual CowPipelineData CreateInputPipelineData()
        {
            var source = ConfigurationManager.AppSettings["InputFile"];
            var fs = new FileStream(source, FileMode.Open);
            {
                var cpd = new CowPipelineData();
                cpd.Data = fs;
                cpd.Time = DateTime.Now;
                cpd.Source = source;
                cpd.Index = "default";
                cpd.Host = Dns.GetHostName();
                cpd.SourceType = "file";
                return cpd;
            }
        }

        public virtual bool Execute(CowPipelineData data)
        {
            bool ret = false;
            var processor = new IndexProcessor();
            if (processor != null)
                ret = processor.write(data, data.Time);
            return ret;
        }
    }
}
