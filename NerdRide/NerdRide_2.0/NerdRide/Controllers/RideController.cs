using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdRide.Helpers;
using NerdRide.Models;

namespace NerdRide.Controllers {

    [HandleErrorWithELMAH]
    public class RidesController : Controller {

        IRideRepository RideRepository;

        //
        // Dependency Injection enabled constructors

        public RidesController()
            : this(new RideRepository()) {
        }

        public RidesController(IRideRepository repository) {
            RideRepository = repository;
        }

        //
        // GET: /Rides/
        //      /Rides/Page/2
        //      /Rides?q=term

        public ActionResult Index(string q, int? page) {

            const int pageSize = 25;

            IQueryable<Ride> Rides = null;

            //Searching?
            if (!string.IsNullOrWhiteSpace(q))
                Rides = RideRepository.FindRidesByText(q).OrderBy(d => d.EventDate);
            else 
                Rides = RideRepository.FindUpcomingRides();

            var paginatedRides = new PaginatedList<Ride>(Rides, page ?? 0, pageSize);

            return View(paginatedRides);
        }

        //
        // GET: /Rides/Details/5

        public ActionResult Details(int? id) {
            if (id == null) {
                return new FileNotFoundResult { Message = "No Ride found due to invalid Ride id" };
            }

            Ride Ride = RideRepository.GetRide(id.Value);

            if (Ride == null) {
                return new FileNotFoundResult { Message = "No Ride found for that id" };
            }

            return View(Ride);
        }

        //
        // GET: /Rides/Edit/5

        [Authorize]
        public ActionResult Edit(int id) {

            Ride Ride = RideRepository.GetRide(id);

            if (!Ride.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(Ride);
        }

        //
        // POST: /Rides/Edit/5

        [HttpPost, Authorize]
        public ActionResult Edit(int id, FormCollection collection) {

            Ride Ride = RideRepository.GetRide(id);

            if (!Ride.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            try {
                UpdateModel(Ride, "Ride");

                RideRepository.Save();

                return RedirectToAction("Details", new { id=Ride.RideID });
            }
            catch {
                return View(Ride);
            }
        }

        //
        // GET: /Rides/Create

        [Authorize]
        public ActionResult Create() {

            Ride Ride = new Ride() {
               EventDate = DateTime.Now.AddDays(7)
            };

            return View(new RideFormViewModel(Ride));
        } 

        //
        // POST: /Rides/Create

        [HttpPost, Authorize]
        public ActionResult Create(Ride Ride) {

            if (ModelState.IsValid) {
                NerdIdentity nerd = (NerdIdentity)User.Identity;
                Ride.HostedById = nerd.Name;
                Ride.HostedBy = nerd.FriendlyName;

                RSVP rsvp = new RSVP();
                rsvp.AttendeeNameId = nerd.Name;
                rsvp.AttendeeName = nerd.FriendlyName;
                Ride.RSVPs.Add(rsvp);

                RideRepository.Add(Ride);
                RideRepository.Save();

                return RedirectToAction("Details", new { id=Ride.RideID });
            }

            return View(new RideFormViewModel(Ride));
        }

        //
        // HTTP GET: /Rides/Delete/1

        [Authorize]
        public ActionResult Delete(int id) {

            Ride Ride = RideRepository.GetRide(id);

            if (Ride == null)
                return View("NotFound");

            if (!Ride.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(Ride);
        }

        // 
        // HTTP POST: /Rides/Delete/1

        [HttpPost, Authorize]
        public ActionResult Delete(int id, string confirmButton) {

            Ride Ride = RideRepository.GetRide(id);

            if (Ride == null)
                return View("NotFound");

            if (!Ride.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            RideRepository.Delete(Ride);
            RideRepository.Save();

            return View("Deleted");
        }

  
        protected override void HandleUnknownAction(string actionName)
        {
            throw new HttpException(404, "Action not found");
        }

        public ActionResult Confused()
        {
            return View();
        }

        public ActionResult Trouble()
        {
            return View("Error");
        }

        [Authorize]
        public ActionResult My()
        {

            NerdIdentity nerd = (NerdIdentity)User.Identity;
            var userRides = from Ride in RideRepository.FindAllRides()
                              where
                                (
                                String.Equals((Ride.HostedById ?? Ride.HostedBy), nerd.Name)
                                    ||
                                Ride.RSVPs.Any(r => r.AttendeeNameId == nerd.Name || (r.AttendeeNameId == null && r.AttendeeName == nerd.Name)) 
                                )
                              orderby Ride.EventDate
                              select Ride;

            return View(userRides);
        }

        public ActionResult WebSlicePopular()
        {
            ViewData["Title"] = "Popular Nerd Rides";
            var model = from Ride in RideRepository.FindUpcomingRides()
                                        orderby Ride.RSVPs.Count descending
                                        select Ride;
            return View("WebSlice",model.Take(5));
        }

        public ActionResult WebSliceUpcoming()
        {
            ViewData["Title"] = "Upcoming Nerd Rides";
            DateTime d = DateTime.Now.AddMonths(2);
            var model = from Ride in RideRepository.FindUpcomingRides()
                        where Ride.EventDate < d
                        orderby Ride.EventDate descending
                    select Ride;
            return View("WebSlice", model.Take(5));
        }

    }
}