<%@ Page Inherits="System.Web.Mvc.ViewPage<NerdRide.Helpers.PaginatedList<NerdRide.Models.Ride>>" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Upcoming Nerd Rides
</asp:Content>

<asp:Content ID="Masthead" ContentPlaceHolderID="MastheadContent" runat="server">
	<% Html.RenderPartial("Masthead", false); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class='hslice' id='1' >
        <h2 class='entry-title'>Upcoming Rides</h2>
        <a rel='feedurl' href='/Rides/WebSliceUpcoming' style='display:none;' ></a>
        <ul class="upcomingRides">
    
            <% foreach (var Ride in Model) { %>
        
                <li>     
                    <%: Html.ActionLink(Ride.Title, "Details", new { id=Ride.RideID }) %>
                    on 
                    <strong><%: Ride.EventDate.ToString("yyyy-MMM-dd")%> 
                    <%: Ride.EventDate.ToString("HH:mm tt")%></strong>
                    at 
                    <%: Ride.Address + " " + Ride.Country %>
                </li>
        
            <% } %>

        </ul>
    </div>

    <div class="pagination">

        <% if (Model.HasPreviousPage) { %>
        
            <%: Html.RouteLink("<<< Previous Page", 
                               "UpcomingRides", 
                               new { page=(Model.PageIndex-1) }) %>
        
        <% } %>
        
        <% if (Model.HasNextPage) { %>
        
            <%: Html.RouteLink("Next Page >>>", 
                               "UpcomingRides", 
                               new { page = (Model.PageIndex + 1) })%>
        
        <% } %>    

    </div>
    
</asp:Content>


