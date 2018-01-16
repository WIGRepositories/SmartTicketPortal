/*
 * Title:   Travelo - Travel, Tour Booking HTML5 Template - Contact Javascript file
 * Author:  http://themeforest.net/user/soaptheme
 */
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
    $scope.send = function (con, flag) {
       
        if (con.name == null) {
            alert('Please enter Name.');
            return;
        }
        if (con.email == null) {
            alert('Please enter email.');
            return;
        }
        if (con.subject == null) {
            alert('Please enter subject.');
            return;
        }
        if (con.message == null) {
            alert('Please enter message.');
            return;
        }
        var Contactus = {
                      
            name: con.name,
            email: con.email,
            category: con.category,
            subject: con.subject,
            message: con.message,
            flag: 'I',


        }
        var req = {
            url: '/api/Contact/ContactsRequst',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: Contactus
        }

        $http(req).then(function (response) {

            alert("Your Request Successfull!");

            $scope.Group = null;

        }, function (errres) {
            var errdata = errres.data;
            var errmssg = "Your Details Are Incorrect";
            errmssg = (errdata && errdata.ExceptionMessage) ? errdata.ExceptionMessage : errdata.Message;
            $scope.showDialog(errmssg);
        });

        $scope.clearFleetOwnerRequest1 = function () {
            $scope.FleetOwnerRequest1 = null;
        };
    }
});
tjq(document).ready(function($) {
    $("form.contact-form").submit(function(e) {
        e.preventDefault();
        var obj = $(this);
        if (obj.hasClass("disabled")) {
            return false;
        }
        obj.addClass("disabled");
        $.ajax({
            url: obj.attr("action"),
            type: 'post',
            dataType: 'html',
            data: obj.serialize(),
            success: function(r) {
                var msgobj = obj.find(".alert");
                if (r.indexOf("Success") >= 0) {
                    msgobj.removeClass("alert-error");
                    msgobj.addClass("alert-success");
                } else {
                    msgobj.removeClass("alert-success");
                    msgobj.addClass("alert-error");
                }
                msgobj.text(r);
                msgobj.fadeIn();
                obj.removeClass("disabled");
            }
        });
    });
});