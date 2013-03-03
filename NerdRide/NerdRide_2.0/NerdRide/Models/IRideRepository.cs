using System;
using System.Linq;

namespace NerdRide.Models {

    public interface IRideRepository {

        IQueryable<Ride> FindAllRides();
        IQueryable<Ride> FindByLocation(float latitude, float longitude);
        IQueryable<Ride> FindUpcomingRides();
        IQueryable<Ride> FindRidesByText(string q);

        Ride GetRide(int id);

        void Add(Ride Ride);
        void Delete(Ride Ride);
        
        void Save();
    }
}
