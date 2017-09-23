// JavaScript source code
var myapp1 = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])
var mycrtl1 = myapp1.controller('myCtrl', function ($scope, $http, $localStorage) {
   
   
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
    $scope.selectedOp = 0;
    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }
    $scope.Signin = function () {

        var u = $scope.UserName;
        var p = $scope.Password

        if (u == null) {
            $scope.showDialog('Please enter username');
            return;
        }

        if (p == null) {
            $scope.showDialog('Please enter password');
            return;
        }
        var inputcred = { LoginInfo: u, Passkey: p }

        var req = {
            method: 'POST',
            url: '/api/ValidateCredentials/ValidateCredentials',
            data: inputcred
        }

        $http(req).then(function (res) {

            if (res.data.length == 0) {
                alert('invalid credentials');
            }
            else {
                //if the user has role, then get the details and save in session
                $localStorage.uname = res.data[0].FirstName;
                $scope.username = $localStorage.uname;
                $localStorage.userdetails = res.data;
                //  window.location.href = "UI/BookedTicketHistory.html";
                //$uibModal.close();
                if ($scope.selectedOp == 1) {
                    window.location.href = "BookedTicketHistory.html";
                }
                else {
                    window.location.href = "UserProfile.html";
                }
                //switch ($scope.SelectedOp) {
                //    case 1:
                //        window.location.href = "UI/BookedTicketHistory.html";
                //        break;                    
                //    case 2:
                //        window.location.href = "UI/CancelTicket.html";
                //        break;
                //    case 3:
                //        window.location.href = "UI/Feedback.html";
                //        break;
                //    case 4:
                //        window.location.href = "UserProfile.html";
                //        break;
                //    default:
                //        window.location.href = "UserProfile.html";
                //        break;
                //        break;
                //}

            }
        });
    }

    $scope.SignOutUser = function () {
        $localStorage.uname = null;
        $scope.username = null;
        $localStorage.userdetails = null;
    }
    $scope.LogoutUser = function () {
        $localStorage.uname = null;
        $scope.username = null;
        $localStorage.userdetails = null;

        window.location.href = "../index.html";
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