<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Deleted
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ride Deleted</h2>

    <div>
        <p>Your Ride was successfully deleted.</p>
    </div>
    
    <div>
        <p><a href="/Rides">Click for Upcoming Rides</a></p>
    </div>

</asp:Content>
