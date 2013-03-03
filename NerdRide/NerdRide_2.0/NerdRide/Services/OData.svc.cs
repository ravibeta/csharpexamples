using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Xml.Linq;
using NerdRide.Helpers;
using NerdRide.Models;
using NerdRide.Services;
using DataServicesJSONP;

namespace NerdRide
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [JSONPSupportBehavior]
    public class ODataServices : DataService<NerdRideEntities>
    {
        IRideRepository RideRepository;
        //
        // Dependency Injection enabled constructors

        public ODataServices()
            : this(new RideRepository()) {
        }

        public ODataServices(IRideRepository repository)
        {
            RideRepository = repository;
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            //We have thousands of rows so setup server-page
            config.SetEntitySetPageSize("*", 100);

            // We're exposing both Rides and RSVPs for read
            config.SetEntitySetAccessRule("Rides", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("RSVPs", EntitySetRights.AllRead);
            config.SetServiceOperationAccessRule("RidesNearMe", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            #if DEBUG
            config.UseVerboseErrors = true;
            #endif
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            HttpContext context = HttpContext.Current;
            HttpCachePolicy c = context.Response.Cache;
            c.SetCacheability(HttpCacheability.ServerAndPrivate);
            c.SetExpires(context.Timestamp.AddSeconds(30));
            c.VaryByHeaders["Accept"] = true;
            c.VaryByHeaders["Accept-Charset"] = true;
            c.VaryByHeaders["Accept-Encoding"] = true;
            c.VaryByParams["*"] = true;
        }

        [WebGet]
        public IQueryable<Ride> FindUpcomingRides()
        {
            return RideRepository.FindUpcomingRides();
        }

        // http://localhost:60848/Services.svc/RidesNearMe?placeOrZip='12345'
        [WebGet]
        public IQueryable<Ride> RidesNearMe(string placeOrZip)
        {
            if (String.IsNullOrEmpty(placeOrZip)) return null; ;

            LatLong location = GeolocationService.PlaceOrZipToLatLong(placeOrZip);

            var Rides = RideRepository.
                            FindByLocation(location.Lat, location.Long).
                            OrderByDescending(p => p.EventDate);
            return Rides;
        }

    }
}
