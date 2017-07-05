var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage, $uibModal) {


    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }

    $scope.GetSchedules = function () {

        $http.get('/api/schedule/GetSchedules').then(function (response, req) {
            $scope.Schedules = response.data;
        });
    }

});