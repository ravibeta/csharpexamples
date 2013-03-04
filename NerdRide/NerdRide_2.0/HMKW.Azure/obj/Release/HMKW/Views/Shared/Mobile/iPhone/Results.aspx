<%@ Page Inherits="System.Web.Mvc.ViewPage<NerdRide.Helpers.PaginatedList<NerdRide.Models.Ride>>" Language="C#"  %>
<ul title="Results">
      <% foreach (var Ride in Model) { %>
					<li><a href="<%:Url.RouteUrl("PrettyDetails", new { Id = Ride.RideID } ) %>">
						<%: Ride.EventDate.ToString("MMM dd")%> <%: HttpUtility.HtmlEncode(Ride.Description) %>
					</a></li> 
      <% } %>    
      <% if (Model.Count == 0) { %>
       <li>No Nerd Rides found!</li>
      <% } %>
</ul>
