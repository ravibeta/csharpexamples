using System.Web.Mvc;
using NerdRide.Helpers;

namespace NerdRide.Models {
    public class RideFormViewModel {
        
        public Ride Ride { get; private set; }
        public SelectList Countries { get; private set; }

        public RideFormViewModel(Ride Ride) {
            Ride = Ride;
            Countries = new SelectList(PhoneValidator.Countries, Ride.Country);
        }
    }
}
