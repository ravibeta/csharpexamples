<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdRide.Models.Ride>" %>

<% if (Model.IsHostedBy(Context.User.Identity.Name)) { %>

    <%: Html.ActionLink("Edit Ride", "Edit", new { id=Model.RideID })%>
    |
    <%: Html.ActionLink("Delete Ride", "Delete", new { id = Model.RideID })%>    

<% } %>