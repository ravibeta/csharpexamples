<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdRide.Models.RideFormViewModel>" %>

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

<% Html.EnableClientValidation(); %>
<%: Html.ValidationSummary("Please correct the errors and try again.") %>

<% using (Html.BeginForm()) { %>
    <fieldset>

        <div id="RideDiv">

        <p>
            <label for="Title">Ride Title:</label>
            <%: Html.EditorFor(m => Model.Ride.Title) %>
        </p>
        <p>
            <label for="EventDate">Event Date:</label>
            <%: Html.EditorFor(m => m.Ride.EventDate) %>
        </p>
        <p>
            <label for="Description">Description:</label>
            <%: Html.TextAreaFor(m => Model.Ride.Description, 6, 35, null) %>
        </p>
        <p>
            <label for="Address">Address:</label>
            <%: Html.EditorFor(m => Model.Ride.Address)%>
        </p>
        <p>
            <label for="Country">Country:</label>
            <%: Html.DropDownList("Ride.Country", Model.Countries) %>                
        </p>
        <p>
            <label for="ContactPhone">Contact Info:</label>
            <%: Html.EditorFor(m => Model.Ride.ContactPhone)%>
        </p>
        <p>
            <%: Html.HiddenFor(m => Model.Ride.Latitude)%>
            <%: Html.HiddenFor(m => Model.Ride.Longitude)%>
        </p>                 
        <p>
            <input type="submit" value="Save" />
        </p>
            
        </div>
        
        <div id="mapDiv">    
            <% Html.RenderPartial("Map", Model.Ride); %>
            (drag the pin in the map if it doesn't look right)
        </div> 
            
    </fieldset>

<% } %>


<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        NerdRide.EnableMapMouseClickCallback();

        $("#Ride_Address").blur(function (evt) {
            //If it's time to look for an address, 
            // clear out the Lat and Lon
            $("#Ride_Latitude").val("0");
            $("#Ride_Longitude").val("0");
            var address = jQuery.trim($("#Ride_Address").val());
            if (address.length < 1)
                return;
            NerdRide.FindAddressOnMap(address);
        });
    });
//]]>
</script>

