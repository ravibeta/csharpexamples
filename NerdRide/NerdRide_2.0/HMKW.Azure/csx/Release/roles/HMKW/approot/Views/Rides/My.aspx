<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NerdRide.Models.Ride>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Rides
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>My Rides</h2>

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

        <% if (Model.Count() == 0) { %>
           <li>You don't own or aren't registered for any Rides.</li>
        <% } %>


    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadArea" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MastheadContent" runat="server">
</asp:Content>

