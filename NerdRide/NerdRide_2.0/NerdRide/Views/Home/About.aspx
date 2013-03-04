<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    About
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>What is nerdride.cloudapp.net?</h2>
    <div id="about">
         <p><img src="/Content/nerd.jpg" height="200" style="float:left; padding-right:20px" alt="Picture of a huge nerd."/>Are you a huge nerd? Perhaps a geek? No? Maybe a dork, dweeb or wonk. 
         Quite possibly you're just a normal person. Either way, you're a social being. You need to get out for a ride
         to go somewhere, preferably with folks that are like you. Or you can offer a ride for fee or a conversation.</p>
         <p>Enter <a href="http://www.nerdride.cloudapp.net">nerdride.cloudapp.net</a>, for all your event planning needs. We're focused on one thing. Helping the world carpool on demand. </p>
         <p>We're free and fun. <a href="/">Find a Ride near you</a>, or <a href="/Rides/create">host a Ride</a>. Be social.</p>
         <p>We also have blog badges and widgets that you can install on your blog or website so your readers can get involved in the Nerd Ride movement. Well, it's not really a movement. Mostly just geeks riding together, but still.</p>
	</div>
</asp:Content>
