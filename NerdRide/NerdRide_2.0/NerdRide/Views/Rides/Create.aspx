<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<NerdRide.Models.RideFormViewModel>" MasterPageFile="~/Views/Shared/Site.Master"  %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Host a Nerd Ride
</asp:Content>

<asp:Content ID="Create" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Host a Ride</h2>

    <% Html.RenderPartial("RideForm"); %>

</asp:Content>

