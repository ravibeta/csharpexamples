using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdRide.Models;
using DDay.iCal;
using DDay.iCal.Components;
using DDay.iCal.Serialization;
using System.Web.Mvc;
using DDay.iCal.DataTypes;

namespace NerdRide.Helpers
{
    public static class CalendarHelpers
    {
        public static Event RideToEvent(Ride Ride, iCalendar iCal)
        {
            string eventLink = "http://nrddnr.com/" + Ride.RideID;
            Event evt = iCal.Create<Event>();
            evt.Start = Ride.EventDate;
            evt.Duration = new TimeSpan(3, 0, 0);
            evt.Location = Ride.Address;
            evt.Summary = String.Format("{0} with {1}", Ride.Description, Ride.HostedBy);
            evt.AddContact(Ride.ContactPhone);
            evt.Geo = new Geo(Ride.Latitude, Ride.Longitude);
            evt.Url = eventLink;
            evt.Description = eventLink;
            return evt;
        }
    }
}