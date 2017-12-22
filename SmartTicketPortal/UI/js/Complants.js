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
    $scope.saveNew = function (comp, flag) {

        if (comp.Yourname == null) {
            alert('Please Enter Yourname');
            return;
        }
       // if ($scope.imageSrc == null) {
          //  alert('Please Enter Image');
          //  return;
       // }
        //if (comp.Title == null) {
           // alert('Please Enter Title');
           // return;
       // }

       // if (comp.Description == null) {
          //  alert('Please Enter Description');
          //  return;
      //  }
        if (comp.mobilenumber == null) {
            alert('Please Enter mobilenumber');
            return;
        }
        //if (adv.AdvertismentExpiredDate == null) {
        //    alert('Please Enter AdvertismentExpiredDate');
        //    return;
        //}
        if (comp.YourEmail == null) {
            alert('Please Enter YourEmail');
            return;
        }
        if (comp.masage == null) {
            alert('Please Enter masage');
            return;
        }


        var Compalaints = {
            Id: -1,
            Name: comp.Yourname,
           
            PhoneNumber: comp.mobilenumber,
            EmailId: comp.YourEmail,
            Description: comp.masage,
            TicketNo: comp.bookingnumber,
            Category: comp.AdvertismentDate,
            Subject: comp.subject,
            flag: "I"
        }

        var req = {
            method: 'POST',
            url: '/api/Complaints/ComplaintsPortal',
            data: Compalaints
        }
        $http(req).then(function (response) {

            alert("Saved successfully!");

            $scope.Group = null;

        }, function (errres) {
            var errdata = errres.data;
            var errmssg = "your Details Are Incorrect";
            errmssg = (errdata && errdata.ExceptionMessage) ? errdata.ExceptionMessage : errdata.Message;
            alert(errmssg);
        });
        $scope.currGroup = null;
    };
});
