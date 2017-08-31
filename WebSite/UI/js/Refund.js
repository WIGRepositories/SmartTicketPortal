var app = angular.module('myApp', ['ngStorage'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {

    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;

        $scope.emailid = $localStorage.userdetails[0].EmailAddress;

        $http.get('/api/WebsiteUserInfo/GetRefundHistory?emailid=' + $scope.emailid).then(function (response, data) {
            $scope.bookedHistory = response.data;
        });
    } else {
        window.location.href = "../index.html";
    }

    $scope.LogoutUser = function () {
        $localStorage.uname = null;
        $scope.username = null;
        $localStorage.userdetails = null;

        window.location.href = "../index.html";
    }
});