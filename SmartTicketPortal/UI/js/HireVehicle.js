
var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {
    $scope.GetStops = function () {

        $http.get('/api/Stops/GetStops').then(function (response, req) {
            $scope.Stops = response.data;
            $localStorage.Stops = $scope.Stops;
        }, function (data) {

        });

        $http.get('/api/Stops/TypesByGroupId?groupid=3').then(function (res, data) {
            $scope.licenses = res.data;

        });
    }

    $scope.GetHireVehicle = function () {

        $http.get('/api/HireVehicle/GetHireVehicle').then(function (response, req) {
            $scope.Vehicles = response.data;          

        });       
    }



    $scope.GetVehicle = function () {
        if ($scope.S == null) {
            // alert('Please select source.');

            $scope.showDialog('Please select source.');
            return;
        }

        if ($scope.D == null) {
            $scope.showDialog('Please select destination.');
            return;
        }

        $localStorage.src = $scope.S;
        $localStorage.dest = $scope.D;

        //$rootscope.src = $scope.RS;
        //$rootscope.dest = $scope.RD;
        $localStorage.timing = ($scope.timing == 'Now') ? Date() : $scope.timing;

        $localStorage.triptype = $scope.triptype;
        $scope.PSGetHourbasedpricing();

        //$scope.booking = [
        //    { "Id": 1, "Company": "CMP1", "VehicleType": "Indigo", "StartTime": "11:00 AM", "EndTime": "12:30 PM", "Rating": "3", "Price": "10", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 2, "Company": "CMP2", "VehicleType": "BMW",  "StartTime": "11:10 AM", "EndTime": "12:30 PM", "Rating": "4", "Price": "12", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 3, "Company": "CMP3", "VehicleType": "AC",  "StartTime": "12:00 PM", "EndTime": "12:30 PM", "Rating": "3", "Price": "10", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 4, "Company": "CMP4", "VehicleType": "Non-AC", "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "13", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 5, "Company": "CMP5", "VehicleType": "AC",  "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "11", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 6, "Company": "CMP6", "VehicleType": "AC",  "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "11", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 7, "Company": "CMP7", "VehicleType": "AC",  "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "12", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 8, "Company": "CMP8", "VehicleType": "AC", "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "11", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 9, "Company": "CMP9", "VehicleType": "Non-AC", "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "10", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 10, "Company": "CMP10", "VehicleType": "Non-AC", "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "11", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 11, "Company": "CMP11", "VehicleType": "AC",  "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "13", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 12, "Company": "CMP12", "VehicleType": "AC",  "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "12", "luggageCrg": "", "Discount": "" }
        //    , { "Id": 13, "Company": "CMP13", "VehicleType": "AC", "StartTime": "12:30 PM", "EndTime": "12:30 PM", "Rating": "", "Price": "11", "luggageCrg": "", "Discount": "" }
        //];

    }
    $scope.FillInitialDetails = function (indx, vDetails) {

        if ($scope.selectedIndex != -1)
            document.getElementById('t_' + $scope.selectedIndex).style.display = "none";

        var currstyle = document.getElementById('t_' + indx).style.display;
        // var currstyle = document.getElementById('imgTd').style.display;

        if (currstyle == "none") {
            document.getElementById('t_' + indx).style.display = "table-cell";
            $scope.selectedIndex = indx;
            // $scope.selectedSeats = [];
        }
        //   $scope.basePrice = b.amount;
        $scope.onwarddetails = new Object();
        $scope.onwarddetails.VehicleId = vDetails.Id;
       // $scope.onwarddetails.TicketNo = $scope.GetTicketNo();
        $scope.onwarddetails.JourneyDate = new Date();
        $scope.onwarddetails.JourneyTime = new Date();

        $scope.onwarddetails.Src = $localStorage.src.Name;
        $scope.onwarddetails.Dest = $localStorage.dest.Name;
        $scope.onwarddetails.SrcId = $localStorage.src.Id;
        $scope.onwarddetails.DestId = $localStorage.dest.Id;

        $scope.onwarddetails.EmailId = '';
        $scope.onwarddetails.MobileNo = '';
        $scope.onwarddetails.AltMobileNo = '';
        $scope.onwarddetails.Address = '';
        $scope.onwarddetails.perunitprice = '';
        $scope.onwarddetails.JourneyType = 'Onward';
        $scope.onwarddetails.bookingType = 'Online';
        $scope.onwarddetails.bookedBy = 'Onward';
        $scope.onwarddetails.userid = 'Onward';
        $scope.onwarddetails.NoOfSeats = 0;
        $scope.onwarddetails.seatslist = '';
        $scope.onwarddetails.amount = 0;//
        $scope.onwarddetails.insupddelflag = 'I';//
        $scope.onwarddetails.BookedSeats = [];
    }
    $scope.GetHourBasePricing = function () {
      //  $scope.vm = $localStorage.vm;
      //  $scope.Price = $localStorage.Price;
      // // $scope.timing = $localStorage.timing;


      //  $scope.VehicleModelId = $localStorage.vm.Id;
      ////  $scope.destId = $localStorage.dest.Id;
      //  $scope.srcStage = $localStorage.src.name;
      //  $scope.destStage = $localStorage.dest.name;
      //  $scope.triptype = $localStorage.triptype;


        $http.get('/api/HourBasedPricing/GetHourBasePricing').then(function (response, req) {
            $scope.booking = response.data;
        });
    }
});
