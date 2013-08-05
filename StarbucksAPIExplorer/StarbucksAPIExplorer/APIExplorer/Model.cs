using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APIExplorer
{
    [Serializable]
    public class token
    {
        [XmlElement("access_token")]
        public string access_token { get; set; }
        [XmlElement("expires_in")]
        public string expires_in { get; set; }
        [XmlElement("extended")]
        public string extended { get; set; }
        [XmlElement("refresh_token")]
        public string refresh_token { get; set; }
        [XmlElement("return_type")]
        public string return_type { get; set; }
        [XmlElement("scope")]
        public string scope { get; set; }
        [XmlElement("state")]
        public string state { get; set; }
        [XmlElement("token_type")]
        public string token_type { get; set; }
        [XmlElement("uri")]
        public string uri { get; set; }
    }
}
