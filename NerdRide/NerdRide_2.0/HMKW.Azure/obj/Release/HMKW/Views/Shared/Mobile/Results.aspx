<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NerdRide.Models.Ride>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Results
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nerd Rides</h2>
		<ul class="Rides">
    <% foreach (var Ride in Model) { %>
        <li>     
            <ul>
            <li>
	            <a href="<%: Url.RouteUrl("PrettyDetails", new { Id = Ride.RideID }) %>"><%:Ride.Title %></a>
            </li>
            <li>
		          <%:Ride.EventDate.ToString("yyyy-MMM-dd")%> 
							@
							<%: Ride.EventDate.ToString("h:mm tt")%>
            </li>
						</ul>

        </li>
        <% } %>
      <% if (Model.Count() == 0) { %>
       <li>No Nerd Rides found!</li>
      <% } %>

		</ul>
    <p>
        <%: Html.ActionLink("Back", "Index", "Home") %>
    </p>

</asp:Content>

