var app = angular.module('myApp', ['ngStorage'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {

    if ($localStorage.uname) {
        $scope.UserName = $localStorage.uname;

        $scope.Mobile = $localStorage.userdetails[0].Mobile;

        $http.get('/api/websiteuserinfo/GetWebsiteUserInfo?logininfo=' + $scope.Mobile).then(function (response, data) {
            $scope.userdetails = response.data[0];

        });
        $scope.emailid = $localStorage.userdetails[0].EmailAddress;
        $scope.GetHistory = function () {
            $http.get('/api/WebsiteUserInfo/GetBookedHistory?emailid=' + $scope.emailid).then(function (response, data) {
                $scope.bookedHistory = response.data;

            });
        }
    } else {
       window.location.href = "../index.html";
    }

    $scope.LogoutUser = function () {
        $localStorage.uname = null;
        $scope.UserName = null;
        $localStorage.userdetails = null;

        window.location.href = "../index.html";
    }
});