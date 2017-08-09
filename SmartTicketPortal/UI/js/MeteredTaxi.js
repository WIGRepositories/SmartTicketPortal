app.controller('mapCtrl', function ($scope, $http) {

    var mapOptions = {
        zoom: 8,
        center: new google.maps.LatLng(17.3850, 78.4867),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);

    $scope.markers = [];
    $scope.cities = [];

    var infoWindow = new google.maps.InfoWindow();

    $http.get('http://localhost:1476/api/Tracking/GetLatLongHistory').
        success(function (data) {

            $scope.cities = data;
            $scope.cities.forEach(function (city) {
                createMarker(city);
            });

        });

    var createMarker = function (city) {
        var marker = new google.maps.Marker({
            map: $scope.map,
            position: new google.maps.LatLng(city.Latitude, city.Longitude),
            title: city.city

        });
        marker.content = '<div class="infoWindowContent">' + city.description + '</div>';

        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
            infoWindow.open($scope.map, marker);
        });

        $scope.markers.push(marker);
    };

});