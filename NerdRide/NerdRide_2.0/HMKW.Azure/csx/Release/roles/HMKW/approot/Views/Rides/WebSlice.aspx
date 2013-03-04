<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NerdRide.Models.Ride>>" ContentType="text/html" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html401/strict.dtd">

<html>
<head>
    <title><%: ViewData["Title"] %><</title>
</head>
<body style="margin: 0">
    <div class="hslice" id="webslice" style="width: 320px">
        <div class="entry-content">
            <center><h2 class="entry-title"><%: ViewData["Title"] %></h2></center>
            <div>
                <ul>
                    <% foreach (var Ride in Model) { %>
                    <li style="list-style-type: none;">
                        <%: Html.ActionLink(Ride.Title, "Details", new { id=Ride.RideID }) %>
                        on <strong>
                            <%: Ride.EventDate.ToString("yyyy-MMM-dd")%>
                            <%: Ride.EventDate.ToString("HH:mm tt")%></strong> at
                        <%: Ride.Address + " " + Ride.Country %>
                    </li>
                    <% } %>
                </ul>
            </div>
        </div>
        <a rel="Bookmark" href="http://www.nerdride.cloudapp.net" style="display:none;"></a>
    </div>
</body>
</html>
