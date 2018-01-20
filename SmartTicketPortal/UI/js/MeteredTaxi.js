var app = angular.module('myApp', ['google-maps', 'vsGoogleAutocomplete']);
app.controller('mapCtrl', function ($scope, $http) {

    //$scope.map = {
    //    control: {},
    //    center: {
    //        latitude: 17.3850,
    //        longitude: 78.4867
    //    },
    //    zoom: 20
    //};

    //$scope.map = {
    //    control: {},
    //    center: {
    //        latitude: 17.3850,
    //        longitude: 78.4867
    //    },
    //    zoom: 16
    //};
    var mapOptions = {
        zoom: 16,
        center: new google.maps.LatLng(17.3850, 78.4867),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);

    $scope.markers = [];
    $scope.cities = [];

    var directionsDisplay = new google.maps.DirectionsRenderer();
    var directionsService = new google.maps.DirectionsService();
    var geocoder = new google.maps.Geocoder();


    $scope.getDirections = function () {
        var request = {
            origin: $scope.directions.origin,
            destination: $scope.directions.destination,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                directionsDisplay.setMap($scope.map);
                // directionsDisplay.setPanel(document.getElementById('distance').innerHTML += response.routes[0].legs[0].distance.value + " meters");
                $scope.distText = response.routes[0].legs[0].distance.text;
                $scope.distval = response.routes[0].legs[0].distance.value;
                //response.routes[0].bounds["f"].b
                //17.43665
                //response.routes[0].bounds["b"].b
                //78.41263000000001


                //response.routes[0].bounds["f"].f
                //17.45654
                //response.routes[0].bounds["b"].f
                //78.44829                

                $scope.srcLat = response.routes[0].bounds["f"].b;
                $scope.srcLon = response.routes[0].bounds["b"].b;
                $scope.destLat = response.routes[0].bounds["f"].f;
                $scope.destLon = response.routes[0].bounds["b"].f;
                $scope.directions.showList = true;
            } else {
                alert('Google route unsuccesfull!');
            }

        });
    }

    var infoWindow = new google.maps.InfoWindow();

    $scope.getDirections = function () {
        //get the source latitude and longitude
        //get the target latitude and longitude
        $scope.srcLat = $scope.pickupPoint.place.geometry.location.lat();
        $scope.srcLon = $scope.pickupPoint.place.geometry.location.lng();
        $scope.destLat = $scope.dropPoint.place.geometry.location.lat();
        $scope.destLon = $scope.dropPoint.place.geometry.location.lng();

        $scope.srcName = $scope.pickupPoint.place.name;
        $scope.destName = $scope.dropPoint.place.name;
        //alert($scope.dropPoint.place.geometry.location.lat);
        var request = {
            origin: new google.maps.LatLng($scope.srcLat, $scope.srcLon),//$scope.directions.origin,
            destination: new google.maps.LatLng($scope.destLat, $scope.destLon),//$scope.directions.destination,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                directionsDisplay.setMap($scope.map);
                // directionsDisplay.setPanel(document.getElementById('distance').innerHTML += response.routes[0].legs[0].distance.value + " meters");

                $scope.distval = response.routes[0].legs[0].distance.value / 1000;
                $scope.distText = $scope.distval + " KM";

                //response.routes[0].bounds["f"].b
                //17.43665
                //response.routes[0].bounds["b"].b
                //78.41263000000001


                //response.routes[0].bounds["f"].f
                //17.45654
                //response.routes[0].bounds["b"].f
                //78.44829                

                //$scope.srcLat = response.routes[0].bounds["f"].b;
                //$scope.srcLon = response.routes[0].bounds["b"].b;
                //$scope.destLat = response.routes[0].bounds["f"].f;
                //$scope.destLon = response.routes[0].bounds["b"].f;              

                //$scope.directions.showList = true;
            } else {
                alert('Google route unsuccesfull!');
            }

        });
    }
    $scope.SetTotal = function () {
        $scope.total = eval($scope.unitprice) * eval($scope.distval);
    }
    //-----------------Hidestart-------------------
    $scope.IsVisible = false;
    $scope.ShowHide = function () {
        //If DIV is visible it will be hidden and vice versa.
        $scope.IsVisible = $scope.IsVisible ? false : true;
    }
    //-----------------Hideend-------------------

    //$http.get('http://localhost:1476/api/Tracking/GetLatLongHistory').
    //    success(function (data) {

    //        $scope.cities = data;
    //        $scope.cities.forEach(function (city) {
    //            createMarker(city);
    //        });

    //    });

    //var createMarker = function (city) {
    //    var marker = new google.maps.Marker({
    //        map: $scope.map,
    //        position: new google.maps.LatLng(city.Latitude, city.Longitude),
    //        title: city.city

    //    });
    //    marker.content = '<div class="infoWindowContent">' + city.description + '</div>';

    //    google.maps.event.addListener(marker, 'click', function () {
    //        infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
    //        infoWindow.open($scope.map, marker);
    //    });

    //    $scope.markers.push(marker);
    //};

});