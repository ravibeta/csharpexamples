using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DDay.iCal.Components;
using NerdRide.Helpers;
using DDay.iCal;
using DDay.iCal.Serialization;
using NerdRide.Models;

namespace NerdRide.Controllers
{
    class iCalResult : FileResult
    {
        public List<Ride> Rides { get; set; }

        public iCalResult(string filename) : base("text/calendar")
        {
            this.FileDownloadName = filename;
        }

        public iCalResult(List<Ride> Rides, string filename) : this(filename)
        {
            this.Rides = Rides;
        }

        public iCalResult(Ride Ride, string filename) : this(filename)
        {
            this.Rides = new List<Ride>();
            this.Rides.Add(Ride);
        }

        protected override void WriteFile(System.Web.HttpResponseBase response)
        {
            iCalendar iCal = new iCalendar();
            foreach (Ride d in this.Rides)
            {
                try
                {
                    Event e = CalendarHelpers.RideToEvent(d, iCal);
                    iCal.Events.Add(e);
                }
                catch (ArgumentOutOfRangeException)
                { 
                    //Swallow folks that have Rides in 9999. 
                }
            }

            iCalendarSerializer serializer = new iCalendarSerializer(iCal);
            string result = serializer.SerializeToString();
            response.ContentEncoding = Encoding.UTF8;
            response.Write(result);
        }

    }
}
