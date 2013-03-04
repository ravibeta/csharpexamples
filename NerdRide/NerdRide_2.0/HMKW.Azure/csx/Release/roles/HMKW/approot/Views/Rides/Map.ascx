<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdRide.Models.Ride>" %>

<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>

<script src="/Scripts/NerdRide.js" type="text/javascript"></script>

<div id="theMap" style="width:520px"></div>
<script type="text/javascript">
//<![CDATA[   
    $(document).ready(function() {
        var latitude = <%: Convert.ToString(Model.Latitude, CultureInfo.InvariantCulture) %>;
        var longitude = <%: Convert.ToString(Model.Longitude, CultureInfo.InvariantCulture) %>;
                
        if ((latitude == 0) || (longitude == 0))
            NerdRide.LoadMap();
        else
            NerdRide.LoadMap(latitude, longitude, mapLoaded);
    });
      
    function mapLoaded() {
        var title = "<%: Model.Title %>";
        var address = "<%: Model.Address %>";
    
        NerdRide.LoadPin(NerdRide._map.GetCenter(), title, address);
        NerdRide._map.SetZoomLevel(14);
    } 
//]]>
</script>
