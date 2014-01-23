using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogOwner.Models
{
    public class Dog
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public byte[] Image { get; set; }
    }
}