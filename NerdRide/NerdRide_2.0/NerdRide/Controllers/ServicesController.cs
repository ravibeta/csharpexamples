using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdRide.Models;
using NerdRide.Helpers;
using DDay.iCal.Components;
using NerdRide.Services;
using System.ComponentModel;

namespace NerdRide.Controllers
{
    [HandleErrorWithELMAH]
    public class ServicesController : Controller
    {
        IRideRepository RideRepository;

        public ServicesController() : this(new RideRepository()){}

        public ServicesController(IRideRepository repository)
        {
            RideRepository = repository;
        }

        [OutputCache(VaryByParam = "none", Duration = 300)]
        public ActionResult RSS()
        {
            var Rides = RideRepository.FindUpcomingRides();

            if (Rides == null)
                return View("NotFound");

            return new RssResult(Rides.ToList(), "Upcoming Nerd Rides");
        }

        [OutputCache(VaryByParam = "none", Duration = 300)]
        public ActionResult iCalFeed()
        {
            var Rides = RideRepository.FindUpcomingRides();

            if (Rides == null)
                return View("NotFound");

            return new iCalResult(Rides.ToList(), "NerdRides.ics");
        }
        
        public ActionResult iCal(int id)
        {
            Ride Ride = RideRepository.GetRide(id);

            if (Ride == null)
                return View("NotFound");

            return new iCalResult(Ride, "NerdRide.ics");
        }

        public ActionResult Flair([DefaultValue("html")]string format)
        {
            string SourceIP = string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ?
                Request.ServerVariables["REMOTE_ADDR"] :
                Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            var location = GeolocationService.HostIpToPlaceName(SourceIP);
            var Rides = RideRepository.
                FindByLocation(location.Position.Lat, location.Position.Long).
                OrderByDescending(p => p.EventDate).Take(3);

            // Select the view we'll return. Using a switch because we'll add in JSON and other formats later.
            // Will probably extract or refactor this.
            string view;
            switch (format.ToLower())
            {
                case "javascript":
                    view = "JavascriptFlair";
                    break;
                default:
                    view = "Flair";
                    break;
            }

            return View(
                view,
                new FlairViewModel 
                {
                    Rides = Rides.ToList(),
                    LocationName = string.IsNullOrEmpty(location.City) ? "you" :  String.Format("{0}, {1}", location.City, location.RegionName)
                }
            );
        }
    }
}
