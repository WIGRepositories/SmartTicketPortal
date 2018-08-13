// JavaScript source code
var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])
var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage, $uibModal) {
    $scope.selectedOp = 0;
    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }
    $scope.licenseCatId = 7;//$localStorage.licenseId;
    $scope.FleetOwnerCode = $localStorage.code;
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
    /*the below function gets all the configured licenses for the given category*/
    $scope.showBuyBtn = 0;
    $scope.showRenewBtn = 0;
    $scope.ValidateLicenseCode = function (lcode) {

        if (lcode == null) {
            return false;
        }
        else {
            $http.get('/api/fleetownerlicense/validatefleetowner?fleetownercode=' + lcode).then(function (response, req) {
                $scope.foLicenseDetails = response.data;
                if ($scope.foLicenseDetails.Table2[0].result == 0) {
                    $scope.showVDialog('Invalid Fleet Owner Code');
                }
                else {
                    $http.get('/api/UserLicenses/getFleetLicenses?fleetcode=' + lcode).then(function (response, req) {
                        $scope.License = response.data;
                        $scope.lLicense = response.data;
                        $localStorage.License = $scope.License;
                        if ($scope.License == null) {
                            alert('No license details configured for the selected license category. Please contact INTERBUS administartor.');
                            return;
                        }
                    })
                    //$localStorage.foLicenseDetails = $scope.foLicenseDetails;
                    //$scope.saveUserLicense($scope.License);
                    //window.location.href = "Cartdetails.html";
                }
            });
        }
    };
    $scope.CheckOut = function (dd) {
        $localStorage.Isrenewal = 1;
        var ul = $localStorage.UselicenseRecord;
        //now go to checkout page     
        var userlicense = {
            ULId: dd.Id,
            CreatedOn: null,
            Amount: eval(dd.quantity) * eval(dd.UnitPrice),
            UnitPrice: dd.UnitPrice,
            StatusId: 1,//ch.StatusId,
            LicensePymtTransId: -1,//ch.LicensePymtTransId,
            IsRenewal: 1,//ch.IsRenewal,
            Units: dd.quantity,
            insupddelflag: 'I'
        }
        $http({
            url: '/api/UserLicenses/SaveUserLicensePayment',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: userlicense,

        }).success(function (data, status, headers, config) {
            $localStorage.focheckoutDetails = data.Table;
            $localStorage.UselicensePymtRecord = data.Table1;
            window.location.href = "/UI/CheckOut.html";

        }).error(function (ata, status, headers, config) {
            alert(ata);
        });
    }
    $scope.SetLicensedetails = function (License, Lid) {
        $localStorage.License = License;
        $localStorage.LicenseTypeId = Lid.Id;
        $localStorage.SelLic = Lid;
        $localStorage.Isrenewal = 0;
    };
  
    $scope.LogoutUser = function () {
        $localStorage.uname = null;
        $scope.username = null;
        $localStorage.userdetails = null;

        window.location.href = "../index.html";
    }
    $scope.showVDialog = function (message) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'statusPopup.html',
            controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return message;
                }
            }
        });
    }
});

app.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, mssg) {

    $scope.mssg = mssg;
    $scope.ok = function () {
        $uibModalInstance.close('test');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});
