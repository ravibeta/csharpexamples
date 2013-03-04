<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    NerdRide
</asp:Content>

<asp:Content ID="Masthead" ContentPlaceHolderID="MastheadContent" runat="server">
	<% Html.RenderPartial("Masthead", true); %>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>
<script src="/Scripts/MSAjaxHistoryBundle.js" type="text/javascript"></script>

<h2>Find a Ride</h2>

<div id="mapDivLeft">


    <div id="theMap" style="width: 580px; height: 400px;"></div>

</div>

<div id="mapDivRight">
    <div class='hslice' id='2'>
        <h2 class='entry-title'>Popular Rides</h2>
        <div class="entry-content" id="RideList"></div>
        <a rel='feedurl' href='/Rides/WebSlicePopular' style='display:none;'></a>
    </div>
</div>

<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {

        NerdRide.LoadMap();

        Sys.Application.set_enableHistory(true);

        Sys.Application.add_navigate(OnNavigation);

        OnNavigation();
    });

    function OnNavigation(sender, args) {
        if (Sys.Application.get_stateString() === '') {
            $get('Location').value = '';
            NerdRide.FindMostPopularRides(8);

            $.getJSON('http://ipinfodb.com/ip_query.php?&output=json&timezone=false&callback=?',
            function (data) {
                if (data.RegionName != '') {
                    $get('Location').value = data.RegionName + ', ' + data.CountryName;
                }
            });
        }
        else {
            var where = Sys.Application._state.where;

            $get('Location').value = where;
            NerdRide.FindRidesGivenLocation(where);
        }
    }

    $("#search").click(ValidateAndFindRides);

    $("#Location").keypress(function(evt) {
        if (evt.which == 13) {
            ValidateAndFindRides();
        }
    });

    function ValidateAndFindRides() {
        var where = $.trim($get('Location').value);
        
        if (where.length < 1)
            return;

        Sys.Application.addHistoryPoint({ 'where': where });

        NerdRide.FindRidesGivenLocation(where);
    }
//]]>
</script>

</asp:Content>
