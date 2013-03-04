function NerdRide() { }

NerdRide.MapDivId = 'theMap';
NerdRide._map = null;
NerdRide._points = [];
NerdRide._shapes = [];

NerdRide.LoadMap = function (latitude, longitude, onMapLoaded) {
    NerdRide._map = new VEMap(NerdRide.MapDivId);

    var options = new VEMapOptions();

    options.EnableBirdseye = false

    // Makes the control bar less obtrusize.
    this._map.SetDashboardSize(VEDashboardSize.Small);

    if (onMapLoaded != null)
        NerdRide._map.onLoadMap = onMapLoaded;

    if (latitude != null && longitude != null) {
        var center = new VELatLong(latitude, longitude);
    }

    NerdRide._map.LoadMap(center, null, null, null, null, null, null, options);
}

NerdRide.ClearMap = function () {
    NerdRide._map.Clear();
    NerdRide._points = [];
    NerdRide._shapes = [];
}

NerdRide.LoadPin = function (LL, name, description) {
    var shape = new VEShape(VEShapeType.Pushpin, LL);

    //Make a nice Pushpin shape with a title and description
    shape.SetTitle("<span class=\"pinTitle\"> " + escape(name) + "</span>");

    if (description !== undefined) {
        shape.SetDescription("<p class=\"pinDetails\">" + escape(description) + "</p>");
    }

    NerdRide._map.AddShape(shape);
    NerdRide._points.push(LL);
    NerdRide._shapes.push(shape);
}

NerdRide.FindAddressOnMap = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    NerdRide._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, NerdRide._callbackForLocation);
}

NerdRide._callbackForLocation = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    NerdRide.ClearMap();

    if (places == null) {
        NerdRide._map.ShowMessage(VEErrorMessage);
        return;
    }

    //Make a pushpin for each place we find
    $.each(places, function (i, item) {
        var description = "";
        if (item.Description !== undefined) {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude,
                        item.LatLong.Longitude);

        NerdRide.LoadPin(LL, item.Name, description);
    });

    //Make sure all pushpins are visible
    if (NerdRide._points.length > 1) {
        NerdRide._map.SetMapView(NerdRide._points);
    }

    //If we've found exactly one place, that's our address.
    //lat/long precision was getting lost here with toLocaleString, changed to toString
    if (NerdRide._points.length === 1) {
        $("#Latitude").val(NerdRide._points[0].Latitude.toString());
        $("#Longitude").val(NerdRide._points[0].Longitude.toString());
    }
}

NerdRide.FindRidesGivenLocation = function (where) {
    NerdRide._map.Find("", where, null, null, null, null, null, false,
                         null, null, NerdRide._callbackUpdateMapRides);
}


NerdRide.FindMostPopularRides = function (limit) {
    $.post("/Search/GetMostPopularRides", { "limit": limit }, NerdRide._renderRides, "json");
}

NerdRide._callbackUpdateMapRides = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    var center = NerdRide._map.GetCenter();

    $.post("/Search/SearchByLocation",
           { latitude: center.Latitude, longitude: center.Longitude },
           NerdRide._renderRides,
           "json");
}


NerdRide._renderRides = function (Rides) {
    $("#RideList").empty();

    NerdRide.ClearMap();

    $.each(Rides, function (i, Ride) {

        var LL = new VELatLong(Ride.Latitude, Ride.Longitude, 0, null);

        // Add Pin to Map
        NerdRide.LoadPin(LL, _getRideLinkHTML(Ride), _getRideDescriptionHTML(Ride));

        //Add a Ride to the <ul> RideList on the right
        $('#RideList').append($('<li/>')
                        .attr("class", "RideItem")
                        .append(_getRideLinkHTML(Ride))
                        .append($('<br/>'))
                        .append(_getRideDate(Ride, "mmm d"))
                        .append(" with " + _getRSVPMessage(Ride.RSVPCount)));
    });

    // Adjust zoom to display all the pins we just added.
    if (NerdRide._points.length > 1) {
        NerdRide._map.SetMapView(NerdRide._points);
    }

    // Display the event's pin-bubble on hover.
    $(".RideItem").each(function (i, Ride) {
        $(Ride).hover(
            function () { NerdRide._map.ShowInfoBox(NerdRide._shapes[i]); },
            function () { NerdRide._map.HideInfoBox(NerdRide._shapes[i]); }
        );
    });

    function _getRideDate(Ride, formatStr) {
        return '<strong>' + _dateDeserialize(Ride.EventDate).format(formatStr) + '</strong>';
    }

    function _getRideLinkHTML(Ride) {
        return '<a href="' + Ride.Url + '">' + Ride.Title + '</a>';
    }

    function _getRideDescriptionHTML(Ride) {
        return '<p>' + _getRideDate(Ride, "mmmm d, yyyy") + '</p><p>' + Ride.Description + '</p>' + _getRSVPMessage(Ride.RSVPCount);
    }

    function _dateDeserialize(dateStr) {
        return eval('new' + dateStr.replace(/\//g, ' '));
    }


    function _getRSVPMessage(RSVPCount) {
        var rsvpMessage = "" + RSVPCount + " RSVP";

        if (RSVPCount > 1)
            rsvpMessage += "s";

        return rsvpMessage;
    }
}

NerdRide.dragShape = null;
NerdRide.dragPixel = null;

NerdRide.EnableMapMouseClickCallback = function () {
    NerdRide._map.AttachEvent("onmousedown", NerdRide.onMouseDown);
    NerdRide._map.AttachEvent("onmouseup", NerdRide.onMouseUp);
    NerdRide._map.AttachEvent("onmousemove", NerdRide.onMouseMove);
}

NerdRide.onMouseDown = function (e) {
    if (e.elementID != null) {
        NerdRide.dragShape = NerdRide._map.GetShapeByID(e.elementID);
        return true;
    }
}

NerdRide.onMouseUp = function (e) {
    if (NerdRide.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        NerdRide.dragPixel = new VEPixel(x, y);
        var LatLong = NerdRide._map.PixelToLatLong(NerdRide.dragPixel);
        $("#Latitude").val(LatLong.Latitude.toString());
        $("#Longitude").val(LatLong.Longitude.toString());
        NerdRide.dragShape = null;
        
        NerdRide._map.FindLocations(LatLong, NerdRide.getLocationResults);
    }
}

NerdRide.onMouseMove = function (e) {
    if (NerdRide.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        NerdRide.dragPixel = new VEPixel(x, y);
        var LatLong = NerdRide._map.PixelToLatLong(NerdRide.dragPixel);
        NerdRide.dragShape.SetPoints(LatLong);
        return true;
    }
}

NerdRide.getLocationResults = function (locations) {
    if (locations) {
        var currentAddress = $("#Ride_Address").val();
        if (locations[0].Name != currentAddress) {
            var answer = confirm("Bing Maps returned the address '" + locations[0].Name + "' for the pin location. Click 'OK' to use this address for the event, or 'Cancel' to use the current address of '" + currentAddress + "'");
            if (answer) {
                $("#Ride_Address").val(locations[0].Name);
            }
        }
    }
}