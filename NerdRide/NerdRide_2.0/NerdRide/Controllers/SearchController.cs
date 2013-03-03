using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using NerdRide.Helpers;
using NerdRide.Models;
using NerdRide.Services;

namespace NerdRide.Controllers
{

    public class JsonRide
    {
        public int RideID { get; set; }
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
        public string Url { get; set; }
    }

    [HandleErrorWithELMAH]
    public class SearchController : Controller
    {

        IRideRepository RideRepository;

        //
        // Dependency Injection enabled constructors

        public SearchController()
            : this(new RideRepository())
        {
        }

        public SearchController(IRideRepository repository)
        {
            RideRepository = repository;
        }

        //
        // AJAX: /Search/FindByLocation?longitude=45&latitude=-90

        [HttpPost]
        public ActionResult SearchByLocation(float latitude, float longitude)
        {
            var Rides = RideRepository.FindByLocation(latitude, longitude);

            var jsonRides = from Ride in Rides.AsEnumerable()
                              select JsonRideFromRide(Ride);

            return Json(jsonRides.ToList());
        }

        [HttpPost]
        public ActionResult SearchByPlaceNameOrZip(string placeOrZip)
        {
            if (String.IsNullOrEmpty(placeOrZip)) return null; ;
            LatLong location = GeolocationService.PlaceOrZipToLatLong(placeOrZip);

            var Rides = RideRepository.
                            FindByLocation(location.Lat, location.Long).
                            OrderByDescending(p => p.EventDate);

            return View("Results", new PaginatedList<Ride>(Rides, 0, 20));
        }

     
        //
        // AJAX: /Search/GetMostPopularRides
        // AJAX: /Search/GetMostPopularRides?limit=5

        [HttpPost]
        public ActionResult GetMostPopularRides(int? limit)
        {
            var Rides = RideRepository.FindUpcomingRides();

            // Default the limit to 40, if not supplied.
            if (!limit.HasValue)
                limit = 40;

            var mostPopularRides = from Ride in Rides
                                     orderby Ride.RSVPs.Count descending
                                     select Ride;

            var jsonRides =
                mostPopularRides.Take(limit.Value).AsEnumerable()
                .Select(item => JsonRideFromRide(item));

            return Json(jsonRides.ToList());
        }

        private JsonRide JsonRideFromRide(Ride Ride)
        {
            return new JsonRide
            {
                RideID = Ride.RideID,
                EventDate = Ride.EventDate,
                Latitude = Ride.Latitude,
                Longitude = Ride.Longitude,
                Title = Ride.Title,
                Description = Ride.Description,
                RSVPCount = Ride.RSVPs.Count,

                //TODO: Need to mock this out for testing...
                //Url = Url.RouteUrl("PrettyDetails", new { Id = Ride.RideID } )
                Url = Ride.RideID.ToString()
            };
        }

    }
}