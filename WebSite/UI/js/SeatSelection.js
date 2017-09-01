// JavaScript source code
var myapp1 = angular.module('myApp', [])
var mycrtl1 = myapp1.controller('Mycntrlr', function ($scope, $http) {

    $http.get('http://localhost:1476/api/KESENINE3/commericialsite').then(function (res, data) {
        $scope.tr = res.data;


    })


    {    //This will hide the DIV by default.
        $scope.IsHidden = true;
        $scope.ShowHide = function () {
            //If DIV is hidden it will be visible and vice versa.
            $scope.IsHidden = $scope.IsHidden ? false : true;
        }
    }
});
//$scope.album = [{
//    id: 1,
//    name: 11
//},
//  {
//      id: 2,
//      name: 12
//  },
//  {
//      id: 3,
//      name: 13,
//  },
//  {
//      id: 4,
//      name: 14
//  },

//   {
//       id: 5,
//       name: 15,
//   },

//    {
//        id: 6,
//        name: 16,
//    },


//    {
//        id: 7,
//        name: 17,
//    },

//    {
//        id: 8,
//        name: 18,
//    },
//    {
//        id: 9,
//        name: 19

//  }];

//$scope.save = function (album) {
//    var album = {

//        id: album.id,
//        name: album.name

//    };
//    $scope.albumNameArray = [];
//    angular.forEach($scope.album, function (album) {
//        if (album.selected) $scope.albumNameArray.push(album.name);
//    });

//}
//});