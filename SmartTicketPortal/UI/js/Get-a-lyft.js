var app = angular.module('plunker', ['google-maps']);

app.controller('MainCtrl', function ($scope, $document,$http) {
    // map object
    $scope.map = {
        control: {},
        center: {
            latitude: 17.3850,
            longitude: 78.4867
        },
        zoom: 14
    };

    // marker object
    $scope.marker = {
        center: {
            latitude: 17.3850,
            longitude: 78.4867
        }
    }

    // instantiate google map objects for directions
    var directionsDisplay = new google.maps.DirectionsRenderer();
    var directionsService = new google.maps.DirectionsService();
    var geocoder = new google.maps.Geocoder();

    // directions object -- with defaults
    $scope.directions = {
        origin: "Lampex Electronics Limited, IDA Kukatpally, Hyderabad, Telangana",
        destination: "webingate solutions pvt ltd., Erragadda, Hyderabad, Telangana",
        showList: false
    }

    // get directions using google maps api
    $scope.getDirections = function () {
        var request = {
            origin: $scope.directions.origin,
            destination: $scope.directions.destination,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function (response, status) {
            if (status === google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                directionsDisplay.setMap($scope.map.control.getGMap());

                $scope.distval = response.routes[0].legs[0].distance.value / 1000;
                $scope.distText = $scope.distval + " KM";
                //directionsDisplay.setPanel(document.getElementById('directionsList'));
                //$scope.directions.showList = true;
            } else {
                alert('Google route unsuccesfull!');
            }
            $scope.SetTotal();
        });
        $scope.step = 2;
    }
    

    $scope.step = 1;
    $scope.test = function () {
        $scope.step = 3
    };
    $scope.SetTotal = function () {

        distance: $scope.distText

        var dist = {
            distance: $scope.distText,
            packageId:1
        }

        //$scope.total = eval($scope.unitprice) * eval($scope.distval);
        var req = {
            method: 'POST',
            url: '/api/BookAVehicle/CalculatePrice',
            data: dist
        }
        $http(req).then(function (response) {
            var res = response.data;
        });
    }

    var inputcred = { distance: $scope.distText }

    $scope.SaveNew = function (book) {
        //alert();


        var vdpc = {
            Id: -1,
            Src: $scope.directions.origin,
            Dest: $scope.directions.destination,
            SrcLatitude: $scope.srcLat,
            SrcLongitude: $scope.srcLon,
            DestLatitude: $scope.destLat,
            DestLongitude: $scope.destLon,
            packageId: 1,
            CustomerPhoneNo: book.CustomerPhoneNo,
            BookingStatus: $scope.BookingStatus,
            BookingChannel: $scope.BookingChannel,
            //PricingTypeId: directions.pricing,
            Pricing: $scope.pricing,
            distance: $scope.distText,
            UnitPrice: $scope.unitprice,
            Amount: $scope.Price,
            flag: 'I'
        }

        var req = {
            method: 'POST',
            url: '/api/BookAVehicle/SaveBookingDetails',
            data: vdpc
        }

        $http(req).then(function (response) {

            //alert("Booking successful!");
            $scope.Group = null;

        }, function (errres) {
            var errdata = errres.data;
            var errmssg = "";
            errmssg = (errdata && errdata.ExceptionMessage) ? errdata.ExceptionMessage : errdata.Message;
            $scope.showDialog(errmssg);
        });
    }
   
    
    //$http(req).then(function (res) {

    //    if (res.data.length == 0) {
    //        alert('invalid credentials');
    //    }
    //});
});
