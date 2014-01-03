using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanPolicy.Model
{
    public class User
    {
        public string Name { get; set; }
        public Group Group { get; private set; }
        public DateTime LoginTime { get; private set; }
        public DateTime LogoffTime { get; private set; }
        public static User Create(string name) { throw new NotImplementedException();  }
    }
}
