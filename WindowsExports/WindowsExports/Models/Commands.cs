using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindowsExports.Models
{
    public class Commands
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public byte[] Binary { get; set; }
    }
}
