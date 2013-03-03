using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSearchToolkit;
using NerdRide.Models;

namespace NerdRide
{
    /// <summary>
    /// Summary description for OpenSearch
    /// </summary>
    public class OpenSearch : OpenSearchHandler
    {
        protected override Description Description
        {
            get
            {
                return new Description
                {
                    DisplayName = "NerdRide.com",
                    LongDescription = "Nerd Ride - Organizing the world's nerds and helping them eat in packs",
                    SearchPathTemplate = "/Rides?q={0}",
                    IconPath = "~/favicon.ico"
                };
            }
        }

        protected override IEnumerable<SearchResult> GetResults(string q)
        {
            var Rides = new RideRepository().FindRidesByText(q).ToArray();

            return from Ride in Rides
                   select new
                       SearchResult
                       {
                           Description = Ride.Description,
                           Title = Ride.Title + " with " + Ride.HostedBy,
                           Path = "/" + Ride.RideID
                       };
        }

        protected override IEnumerable<SearchSuggestion> GetSuggestions(string term)
        {
            var Rides = new RideRepository().FindRidesByText(term).ToArray();

            return from Ride in Rides
                   select new
                       SearchSuggestion
                   {
                       Description = Ride.Description,
                       Term = Ride.Title + " with " + Ride.HostedBy,
                   };
        }

        protected override bool SupportsSuggestions
        {
            get { return true; }
        }
    }
}