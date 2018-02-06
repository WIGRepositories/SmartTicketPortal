var app = angular.module('myApp', ['ngStorage'])
var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {

    $scope.selectedOp = 0;
    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }
   
    $scope.GetInventoryItems = function () {

        $http.get('/api/Inventory/GetInventoryItem?subCatId=-1').then(function (response, req) {
            $scope.InventoryItems = response.data;
            //  $scope.getselectval();

        });
    }

    $scope.GetHireVehicle = function () {

        $http.get('/api/HireVehicle/GetHireVehicle?srcId=1&destId=1').then(function (response, req) {
            $scope.Vehicles = response.data;

        });
    }
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

    $scope.SaveNew = function (app, flag) {

        if (app.Username == null) {
            alert('Please Enter Username');
            return;
        }

        if (app.Firstname == null) {
            alert('Please Enter Firstname');
            return;
        }
        if (app.lastname == null) {
            alert('Please Enter lastname');
            return;
        }
        if (app.Email == null) {
            alert('Please Enter Email');
            return;
        }
        if (app.Mobilenumber == null) {
            alert('Please Enter Mobilenumber');
            return;
        }

        var app = {

            flag: 'I',
            Id: -1,
            Username: app.Username,
            Firstname: app.Firstname,
            lastname: app.lastname,
            Email: app.Email,
            Mobilenumber: app.Mobilenumber,
            Photo: $scope.imageSrc,
            Altemail: app.Altemail,
            Gender: app.Gender.Id,
            Status: app.Status.Id

        }

        var req = {
            method: 'POST',
            url: '/api/RegisterUser/Appusers',
            data: app
        }
        $http(req).then(function (response) {

            alert("Saved successfully!");
            $scope.GetUsers();
            $scope.Group = null;
            //$scope.GetVehcileMaster('VID=1');
            //window.location.href = "vehicleDetails.html?VID=1";
        }, function (errres) {
            var errdata = errres.data;
            var errmssg = "Your Details Are Incorrect";
            errmssg = (errdata && errdata.ExceptionMessage) ? errdata.ExceptionMessage : errdata.Message;
            alert(errmssg);
        });
        $scope.currGroup = null;
    };
    //-----------------Hidestart-------------------
    $scope.IsVisible = false;
    $scope.ShowHide = function () {
        //If DIV is visible it will be hidden and vice versa.
        $scope.IsVisible = $scope.IsVisible ? false : true;
    }
    //-----------------Hideend-------------------
});
