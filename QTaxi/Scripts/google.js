
function gAttachPlaceApiByClassName(className, callBack) {
    $("." + className).each(function () {
        var inputElement = $(this);
        var elId = $(this).attr("id")
        var places = new google.maps.places.Autocomplete(document.getElementById(elId));
        google.maps.event.addListener(places, 'place_changed', function () {

            var place = places.getPlace();
            var latitude = place.geometry.location.A;
            var longitude = place.geometry.location.F;
            var pos = new google.maps.LatLng(latitude, longitude);
            inputElement.attr("geo", JSON.stringify(pos));
            //var address = place.formatted_address;
            //var latitude = place.geometry.location.k;
            //var longitude = place.geometry.location.D;
            //var mesg = "Address: " + address;
            //field.attr("latitude", latitude);
            //field.attr("longitude", longitude);
            
            var param = {
                elementId: elId,
                pos: { latitude: latitude, longitude: longitude },
                place:place
            }

            if (callBack) {
                callBack(param);
            }
        });
    });
}

function gInitializeMap(elId,option) {
    var mapOptions = {
        zoom: 11
    };
    if (!option)
    {
        option = mapOptions;
    }
   var map = new google.maps.Map(document.getElementById(elId),mapOptions);

    // Try HTML5 geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                             position.coords.longitude);

            var infowindow = new google.maps.InfoWindow({
                map: map,
                position: pos,
                content: 'You are here.'
            });

            map.setCenter(pos);
        }, function () {
            handleNoGeolocation(true);
        });
    } else {
        // Browser doesn't support Geolocation
        handleNoGeolocation(false);
    }

    google.maps.event.addListener(map, "rightclick", function (event) {
        var lat = event.latLng.lat();
        var lng = event.latLng.lng();
        // populate yor box/field with lat, lng
        alert("Lat=" + lat + "; Lng=" + lng);
    });

    return map;
}

function gCalculateDistances() {
    var service = new google.maps.DistanceMatrixService();
    var elFrom = $("#address_from");
    var elTo = $("#address_to");
    var from = new google.maps.LatLng(elFrom.attr("latitude"), elFrom.attr("latitude"))
    service.getDistanceMatrix(
      {
          origins: [elFrom.val()],
          destinations: [elTo.val()],
          travelMode: google.maps.TravelMode.DRIVING,
          unitSystem: google.maps.UnitSystem.METRIC,
          avoidHighways: false,
          avoidTolls: false
      }, calcDistanceCallBack);
}

function handleNoGeolocation(errorFlag) {
    if (errorFlag) {
        var content = 'Error: The Geolocation service failed.';
    } else {
        var content = 'Error: Your browser doesn\'t support geolocation.';
    }

    var options = {
        map: map,
        position: new google.maps.LatLng(60, 105),
        content: content
    };

    var infowindow = new google.maps.InfoWindow(options);
    map.setCenter(options.position);
}