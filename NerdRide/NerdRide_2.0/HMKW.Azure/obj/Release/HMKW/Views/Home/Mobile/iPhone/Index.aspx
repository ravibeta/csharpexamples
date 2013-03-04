<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/iPhone/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NerdRide
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="toolbar">
        <h1 id="pageTitle"></h1>
        <a id="backButton" class="button" href="#"></a>
    </div>
    <form title="NerdRide" class="panel" action="/Search/SearchByPlaceNameOrZip" method="POST" name="Rideform" id="Rideform" selected="true">
        <a class="logo"></a>
        <h2>Find a Nerd Ride near you!</h2>
        <fieldset>
            <div class="row">
                <label>Where</label>
                <input type="text" name="placeOrZip" id="placeOrZip" maxlength="56" onclick="this.value = ''" value="" />
           </div>
        </fieldset>
		<a class="whiteButton" type="submit" href="#">Go</a>
    </form>

</asp:Content>
