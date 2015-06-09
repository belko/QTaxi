var ordersApp = angular.module('ordersApp', []);

ordersApp.controller('OrdersCtrl', function ($scope, $http) {

    $scope.newOrders = [];
    $scope.closedOrders = [];
    $scope.activeOrders = [];
    $scope.cars = [];
    var newOrderTemplate = { address_from: "", address_to: "", id: 0, status: "New", status_id: 1 };
    var markerFrom = null;
    $scope.currentOrder = clone(newOrderTemplate);

    var placesFrom = new google.maps.places.Autocomplete(document.getElementById("newOrderAddressFrom"));
    google.maps.event.addListener(placesFrom, 'place_changed', function () {
        var place = placesFrom.getPlace();
        $scope.currentOrder.address_from = place.formatted_address;
        var lat = place.geometry.location.A;
        var lon = place.geometry.location.F;
        $scope.currentOrder.address_from_lat = lat;
        $scope.currentOrder.address_from_lon = lon;
        var pos = new google.maps.LatLng(lat, lon);
        if (markerFrom) {
            markerFrom.setPosition(pos);
        } else
        {
            markerFrom = new google.maps.Marker({
                position: pos,
                map: gmap,
                title: 'Address from'
            });
            markerFrom.setDraggable(true);
        }
        if (place.address_components.length < 4)
            gmap.setZoom(11)
        else
            if (place.address_components.length <= 5)
                gmap.setZoom(14)
            else 
                gmap.setZoom(17);
        gmap.panTo(pos);
        console.log(place);
    });

    var placesTo = new google.maps.places.Autocomplete(document.getElementById("newOrderAddressTo"));
    google.maps.event.addListener(placesTo, 'place_changed', function () {
        var place = placesTo.getPlace();
        $scope.currentOrder.address_to = place.formatted_address;
        console.log(place);
    });


    $http.get('/api/Orders').success(function (data) {
        $scope.orders = data;
        
        for (var i = 0; i < data.length; i++)
        {
            if (data[i].status == "New") {
                $scope.newOrders.push(data[i]);
            } else
                if (data[i].status == "Finish") {
                    $scope.closedOrders.push(data[i]);
                } else
                    $scope.activeOrders.push(data[i]);
        }
        
    });
    $http.get('/api/Cars').success(function (data) {
        for (var i = 0; i < data.length; i++) {
            $scope.cars.push(data[i]);
        }

    });
    $scope.takeOrder = function (index) {
        $scope.newOrders[index].status = "Wait";
        $scope.activeOrders.push($scope.newOrders[index]);
        $scope.newOrders.splice(index, 1);
    }
    $scope.createOrder = function ()
    {
        
        $scope.currentOrder.status = "New";
        var newOrder = $scope.currentOrder;
        console.log($scope.currentOrder);
        //$.ajax({
        //    type: "POST",
        //    url: "/api/Orders",
        //    data:  newOrder,
        //    dataType: "json",
        //    success: function (data) {
        //        $scope.newOrders.push($scope.currentOrder);
        //        $scope.currentOrder = clone(newOrderTemplate);
        //    },
        //    failure: function (errMsg) {
        //        alert(errMsg);
        //    },
        //    error: function (errMsg) {
        //        alert("ERROR\r\n"+JSON.stringify( errMsg));
        //    }
        //});

        

    }
    function clone(object){
        return jQuery.extend({}, object);
    }


    $scope.setCurrentOrder = function(type, index)
    {

        if (type) {
            switch (type)
            {
                case "new":
                    $scope.currentOrder = $scope.newOrders[index];
                    break;
            }
            
        } else
        {
            $scope.currentOrder = clone(newOrderTemplate);
        }
    }
    
});