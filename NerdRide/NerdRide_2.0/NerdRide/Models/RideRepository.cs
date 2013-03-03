using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data;

namespace NerdRide.Models
{

    public class RideRepository : NerdRide.Models.IRideRepository
    {

        NerdRideEntities db = new NerdRideEntities();

        //
        // Query Methods

        public IQueryable<Ride> FindRidesByText(string q)
        {
            return db.Rides.Where(d => d.Title.Contains(q)
                            || d.Description.Contains(q)
                            || d.HostedBy.Contains(q));
        }

        public IQueryable<Ride> FindAllRides()
        {
            return db.Rides;
        }

        public IQueryable<Ride> FindUpcomingRides()
        {
            return from Ride in FindAllRides()
                   where Ride.EventDate >= DateTime.Now
                   orderby Ride.EventDate
                   select Ride;
        }

        public IQueryable<Ride> FindByLocation(float latitude, float longitude)
        {
            var Rides = from Ride in FindUpcomingRides()
                          join i in NearestRides(latitude, longitude)
                          on Ride.RideID equals i.RideID
                          select Ride;

            return Rides;
        }

        public Ride GetRide(int id)
        {
            return db.Rides.SingleOrDefault(d => d.RideID == id);
        }

        //
        // Insert/Delete Methods

        public void Add(Ride Ride)
        {
            db.Rides.AddObject(Ride);
        }

        public void Delete(Ride Ride)
        {
            foreach (RSVP rsvp in Ride.RSVPs.ToList())
                db.RSVPs.DeleteObject(rsvp);
            db.Rides.DeleteObject(Ride);
        }

        //
        // Persistence 

        public void Save()
        {
            db.SaveChanges();
        }


        // Helper Methods

        [EdmFunction("NerdRideModel.Store", "DistanceBetween")]
        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            throw new NotImplementedException("Only call through LINQ expression");
        }

        public IQueryable<Ride> NearestRides(double latitude, double longitude)
        {
            return from d in db.Rides
                   where DistanceBetween(latitude, longitude, d.Latitude, d.Longitude) < 100
                   select d;
        }
    }
}
