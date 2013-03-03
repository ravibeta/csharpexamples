using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdRide.Models;

namespace NerdRide.Tests.Fakes {

    public class FakeRideRepository : IRideRepository {

        private List<Ride> RideList;

        public FakeRideRepository(List<Ride> Rides) {
            RideList = Rides;
        }

        public IQueryable<Ride> FindRidesByText(string q)        {
            return RideList.AsQueryable().Where(d => d.Title.Contains(q)
                || d.Description.Contains(q)
                || d.HostedBy.Contains(q));
        }

        public IQueryable<Ride> FindAllRides() {
            return RideList.AsQueryable();
        }

        public IQueryable<Ride> FindUpcomingRides() {
            return (from Ride in RideList
                    where Ride.EventDate > DateTime.Now.AddDays(-1)
					orderby Ride.EventDate
                    select Ride).AsQueryable();
        }

        public IQueryable<Ride> FindByLocation(float lat, float lon) {
            return (from Ride in RideList
                    where Ride.Latitude == lat && Ride.Longitude == lon
                    select Ride).AsQueryable();
        }

        public Ride GetRide(int id) {
            return RideList.SingleOrDefault(d => d.RideID == id);
        }

        public void Add(Ride Ride) {
            RideList.Add(Ride);
        }

        public void Delete(Ride Ride) {
            RideList.Remove(Ride);
        }

        public void Save() {
            foreach (Ride Ride in RideList) {
                //TODO: Remove this
                //if (!Ride.IsValid)
                //    throw new ApplicationException("Rule violations");
            }
        }
    }
}
