<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<NerdRide.Models.Ride>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Edit:
    <%:Model.Title %>
</asp:Content>
<asp:Content ID="Edit" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit Ride</h2>
    <% Html.EnableClientValidation(); %>
    <%: Html.ValidationSummary("Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <fieldset>
        <div id="RideDiv">
            <%:Html.EditorForModel() %>
            <p>
                <input type="submit" value="Save" />
            </p>
        </div>
        <div id="mapDiv">
            <%: Html.EditorFor(m => m.Location) %>
        </div>
    </fieldset>
    <% } %>
    <script type="text/javascript">
//<![CDATA[
        $(document).ready(function () {
            NerdRide.EnableMapMouseClickCallback();

            $("#Address").blur(function (evt) {
                //If it's time to look for an address, 
                // clear out the Lat and Lon
                $("#Latitude").val("0");
                $("#Longitude").val("0");
                var address = jQuery.trim($("#Address").val());
                if (address.length < 1)
                    return;
                NerdRide.FindAddressOnMap(address);
            });
            NerdRide.FindAddressOnMap("<%: Model.Location.Address %>");
        });
//]]>
    </script>
</asp:Content>
