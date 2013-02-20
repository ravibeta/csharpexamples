using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDC.Models
{
    public class PersonalDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Male { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BodyFat { get; set; }
        public double Neck { get; set; }
        public double Shoulders { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hip { get; set; }
        public double ThighLeft { get; set; }
        public double ThighRight { get; set; }
        public double CalfLeft { get; set; }
        public double CalfRight { get; set; }
        public double ArmsLeft { get; set; }
        public double ArmsRight { get; set; }
        public double ForeArmLeft { get; set; }
        public double ForeArmRight { get; set; }
    }
}