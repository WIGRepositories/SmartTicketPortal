var app = angular.module('myApp', ['ngStorage'])
var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {

    $scope.TicketsForCancellation = null;

    $scope.getCancellationHistory = function () {

        if ($localStorage.uname) {
            $scope.username = $localStorage.uname;
        }
        else {
            window.location.href = "../index.html";
        }

        $scope.emailid = $localStorage.userdetails[0].EmailAddress;

        $http.get('/api/WebsiteUserInfo/GetCancellationHistory?emailid=' + $scope.emailid).then(function (response, data) {
            $scope.bookedHistory = response.data;
        });
    }


    $scope.LogoutUser = function () {
        $localStorage.uname = null;
        $scope.username = null;
        $localStorage.userdetails = null;

        window.location.href = "../index.html";
    }
    $scope.GetTicketsForCancellation = function () {

        if (($scope.ticketNo == null || $scope.ticketNo == '' ) && ($scope.emailIdmobileno ==null || $scope.emailIdmobileno == '')) {
            alert('Please enter the ticke number or registered email address or mobile number');
        return;
        }
        var tickenum = ($scope.ticketNo == null || $scope.ticketNo == '') ? '' : $scope.ticketNo;
        var em = ($scope.emailIdmobileno == null || $scope.emailIdmobileno == '') ? '' : $scope.emailIdmobileno;

        //get the ticket details and show
        $http.get('/api/TicketBooking/GetTicketsForCancellation?ticketNo=' + tickenum + '&emailmobileno=' + em).then(function (response, data) {
            $scope.TicketsForCancellation = response.data;
        });
    }
});
