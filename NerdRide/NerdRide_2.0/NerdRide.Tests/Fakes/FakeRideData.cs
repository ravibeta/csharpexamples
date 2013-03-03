using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdRide.Models;
using System.Web.Mvc;

namespace NerdRide.Tests.Fakes
{
    class FakeRideData
    {
        public static List<Ride> CreateTestRides()
        {

            List<Ride> Rides = new List<Ride>();

            for (int i = 0; i < 101; i++)
            {

                Ride sampleRide = new Ride()
                {
                    RideID = i,
                    Title = "Sample Ride",
                    HostedBy = "SomeUser",
                    Address = "Some Address",
                    Country = "USA",
                    ContactPhone = "425-555-1212",
                    Description = "Some description",
                    EventDate = DateTime.Now.AddDays(i),
                    Latitude = 99,
                    Longitude = -99
                };

                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = "SomeUser";
                sampleRide.RSVPs.Add(rsvp);

                Rides.Add(sampleRide);
            }

            return Rides;
        }

        public static Ride CreateRide()
        {
            Ride Ride = new Ride();
            Ride.Title = "New Test Ride";
            Ride.EventDate = DateTime.Now.AddDays(7);
            Ride.Address = "5 Main Street";
            Ride.Description = "Desc";
            Ride.ContactPhone = "503-555-1212";
            Ride.HostedBy = "scottgu";
            Ride.Latitude = 45;
            Ride.Longitude = 45;
            Ride.Country = "USA";
            return Ride;
        }

        public static FormCollection CreateRideFormCollection()
        {
            var form = new FormCollection();

            form.Add("Ride.Description", "Description");
            form.Add("Ride.Title", "New Test Ride");
            form.Add("Ride.EventDate", "2010-02-14");
            form.Add("Ride.Address", "5 Main Street");
            form.Add("Ride.ContactPhone", "503-555-1212");
            return form;
        }

    }
}
