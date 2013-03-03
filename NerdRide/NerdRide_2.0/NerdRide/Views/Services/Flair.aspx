<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FlairViewModel>" %>
<%@ Import Namespace="NerdRide.Helpers" %>
<%@ Import Namespace="NerdRide.Models" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Nerd Ride</title>
        <link href="/Content/Flair.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <div id="nd-wrapper">
            <h2 id="nd-header">NerdRide.com</h2>
            <div id="nd-outer">
                <% if (Model.Rides.Count == 0) { %>
                <div id="nd-bummer">
                    Looks like there's no Nerd Rides near
                    <%:Model.LocationName %>
                    in the near future. Why not <a target="_blank" href="http://www.nerdRide.com/Rides/Create">host one</a>?</div>
                <% } else { %>
                <h3>
                    Rides Near You</h3>
                <ul>
                    <% foreach (var item in Model.Rides) { %>
                    <li>
                        <%: Html.ActionLink(String.Format("{0} with {1} on {2}", item.Title.Truncate(20), item.HostedBy, item.EventDate.ToShortDateString()), "Details", "Rides", new { id = item.RideID }, new { target = "_blank" })%></li>
                    <% } %>
                </ul>
                <% } %>
                <div id="nd-footer">
                    More Rides and fun at <a target="_blank" href="http://nerdride.cloudapp.net">http://nerdride.cloudapp.net</a></div>
            </div>
        </div>
    </body>
</html>
