var app = angular.module('myApp', ['ngStorage'])
var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {
    $scope.selectedOp = 0;
    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }
    $scope.FillDetails = function () {
        
            $scope.licenseCatId = $localStorage.LicenseTypeId;
            $scope.focode = $localStorage.FleetOwnerCode;

            //displaying the current fleet owner code
            var fodetails = $localStorage.FleetOwnerCode;
            $scope.selFleetOwnerCode = "";
         
            var ldetails = $localStorage.License;
            $scope.selLicense = "";
            $scope.selLicensePrice = "";
             

            //identify the selected license and display the properties
            for (ltCnt = 0; ltCnt < ldetails.Table.length; ltCnt++) {
                if (ldetails.Table[ltCnt].Id == $scope.licenseCatId) {
                    $scope.selLicense = ldetails.Table[ltCnt];
                    $localStorage.selLicense = $scope.selLicense;
                    break;
                }
            }
            var lfeatures = [];
        //identify the selected license and display the properties
            for (ltCnt = 0; ltCnt < ldetails.Table1.length; ltCnt++) {
                if (ldetails.Table1[ltCnt].LicenseTypeId == $scope.licenseCatId) {
                    lfeatures.push(ldetails.Table1[ltCnt]);
                    if(ldetails.Table1[ltCnt].FeatureTypeId == 22)
                    {
                        $localStorage.noOfBTPOSUnits = ldetails.Table1[ltCnt].FeatureValue;
                    }
                }
            }
            $localStorage.lfeatures = lfeatures;

            for (ltCnt = 0 ; ltCnt < ldetails.Table2.length; ltCnt++) {
                if (ldetails.Table2[ltCnt].LicenseId == $scope.selLicense.Id) {
                    $scope.selLicensePrice = ldetails.Table2[ltCnt]
                }
            }


        //if (FleetOwnerCode != null) {
        //    $scope.selFleetOwnerCode = fodetails.FleetOwnerCode;
        //    document.getElementById("Id").innerHTML = localStorage.getItem("FleetOwnerCode");
        //}

    }
    var checkoutsv = [];
    var focheckout = new Object();
    $scope.CheckOut = function () {

        if ($scope.qty <= 0)
        {
            alert("please select the Unit(s)");
            return;
        }       

        $localStorage.Isrenewal = 1;

        //get other details
        //insert the details int UserLicensePayments
        //if saved successfully
        //in return get the fo details

        var ul = $localStorage.UselicenseRecord;

        //now go to checkout page     

        var userlicense = {                
                   
                   ULId: ul[0].Id,
                   CreatedOn:null,
                   Amount: eval($scope.qty) * eval($scope.selLicensePrice.UnitPrice),
                   UnitPrice: $scope.selLicensePrice.UnitPrice,
                   StatusId:1,//ch.StatusId,
                   LicensePymtTransId:-1,//ch.LicensePymtTransId,
                   IsRenewal:0,//ch.IsRenewal,
                   Units:$scope.qty,
                   insupddelflag: 'I'

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
               
       $http({
           url: '/api/UserLicenses/SaveUserLicensePayment',
          method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: userlicense,

        }).success(function (data, status, headers, config) {
           // alert('Saved successfully');
            $localStorage.focheckoutDetails = data.Table;
            $localStorage.UselicensePymtRecord = data.Table1;
            window.location.href = "/UI/CheckOut.html";
           
        }).error(function (ata, status, headers, config) {
            alert(ata);
        });

       
    }
       

});
           