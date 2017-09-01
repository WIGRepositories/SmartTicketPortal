var app = angular.module('myApp', ['ngStorage'])
var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage, $rootScope) {

    $rootScope.spinner = {
        active: false,
        on: function () {
            this.active = true;
        },
        off: function () {
            this.active = false;
        }
    }

    $scope.book1 = $scope.$localStorage;

    $scope.onwarddetails = $localStorage.onwarddetails;
    $scope.BookingId = $localStorage.BookingId;

    //retrive the emailid and mobile no and try to get the user details from db
    //if not exists then show empty
    $scope.getcheckoutdetails = function () {

        var emailid = $scope.onwarddetails.EmailId;
        $http.get('/api/websiteuserinfo/GetWebsiteUserInfo?logininfo=' + emailid).then(function (response, data) {
            $scope.userdetails = response.data[0];
        });
    }


    $scope.ProceedToPayment = function () {

        $rootScope.spinner.on();

        $scope.onwarddetails = $localStorage.onwarddetails;

        if ($scope.onwarddetails == null) {
            alert('Error occurred during ticket booking. Please retry or contact INTERBUS administrator if the problem persists.');
            return;
        }

        $http({
            url: '/api/TicketBooking/SaveBookingDetails',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: $scope.onwarddetails
        }).success(function (data, status, headers, config) {
            if (data == null) {
                $rootScope.spinner.off();
                alert('Error during ticket booking. Please re-try.')
                return;
            }
            $scope.BookingId = data;

            if ($scope.BookingId == null || $scope.BookingId == -1) {
                $rootScope.spinner.off();
                alert('Error during ticket booking. Please re-try.')
                return;
            }
            $localStorage.BookingId = $scope.BookingId;

            window.location.href = "TicketPage.html";

        }).error(function (ata, status, headers, config) {
            $rootScope.spinner.off();
            alert('Error during ticket booking. Please re-try. Details:' + ata);
        });

        //alert('Payment gateway integration will done here and on successful payment user will be redirect to ticket printing page.');      
    }

});