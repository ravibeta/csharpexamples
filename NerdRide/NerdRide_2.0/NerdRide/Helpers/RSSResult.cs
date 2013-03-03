﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using NerdRide.Models;

namespace NerdRide.Controllers
{
    public class RssResult : FileResult
    {
        public List<Ride> Rides { get; set; }
        public string Title { get; set; }

        private Uri currentUrl;

        public RssResult() : base("application/rss+xml") { }

        public RssResult(List<Ride> Rides, string title) :this()
        {
            this.Rides = Rides;
            this.Title = title;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            currentUrl = context.RequestContext.HttpContext.Request.Url;
            base.ExecuteResult(context);
        }
        protected override void WriteFile(System.Web.HttpResponseBase response)
        {
            var items = new List<SyndicationItem>();

            foreach (Ride d in this.Rides)
            {
                string contentString = String.Format("{0} with {1} on {2:MMM dd, yyyy} at {3}. Where: {4}, {5}",
                            d.Description, d.HostedBy, d.EventDate, d.EventDate.ToShortTimeString(), d.Address, d.Country);
                
                var item = new SyndicationItem(
                    title: d.Title,
                    content: contentString,
                    itemAlternateLink: new Uri("http://nrddnr.com/" + d.RideID),
                    id: "http://nrddnr.com/" + d.RideID,
                    lastUpdatedTime: d.EventDate.ToUniversalTime()
                    );
                item.PublishDate = d.EventDate.ToUniversalTime();
                item.Summary = new TextSyndicationContent(contentString, TextSyndicationContentKind.Plaintext);
                items.Add(item);
            }

            SyndicationFeed feed = new SyndicationFeed(
                this.Title,
                this.Title, /* Using Title also as Description */
                currentUrl, 
                items);

            Rss20FeedFormatter formatter = new Rss20FeedFormatter(feed);

            using (XmlWriter writer = XmlWriter.Create(response.Output))
            {
                formatter.WriteTo(writer);
            }

        }
    }
}