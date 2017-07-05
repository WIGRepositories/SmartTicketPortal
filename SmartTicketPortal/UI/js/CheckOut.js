var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage, $uibModal) {

    $scope.GetDetails = function (TicketDetails) {
        $scope.details = $localStorage.book;
        $scope.FirstName = $scope.details.No_Seats;
    }

    $scope.processPymt = function () {
        var ulD = $localStorage.UselicensePymtRecord[0];
        $('#Modal-header-new').modal('show');

        var UserLicensePymtTransactions = {
            TransId: ulD.TransId,
            GatewayTransId: '-1',
            TransDate: null,
            ULPymtId: ulD.Id,
            Desc: "",
            Tax: 0,
            Discount: 0,
            PymtTypeId: 1,
            Amount: ulD.Amount,
            StatusId: 1,
            LicensePymtTransId: -1,
            insupddelflag: 'I'
        }

        $http({
            url: '/api/UserLicensePymtTransactions/UserLicensePymtTransactions',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: UserLicensePymtTransactions,

        }).success(function (data, status, headers, config) {
            //  alert('Saved successfully');           
            $localStorage.UselicensePymtTranRecord = UserLicensePymtTransactions;

            $http({
                url: '/api/Payments/MakePayment?amt=' + ulD.Amount,
                // url: '/api/Payments/325435',
                method: 'GET'
            }).success(function (data, status, headers, config) {

                var result = data[0].result;
                if (result == 'Failed') {
                    alert('Oops! sorry! Payment could not be processed. Please try again! \n\nbelow are the details of the failure:\n' + data[0].detail + '\n\n if problem continues to persist please contact interbus administrator.')
                    return;
                }
                $localStorage.GatewayTransId = data[0].detail;
                //do the post payment updates

                //  alert('Saved successfully');

                var fo = $localStorage.foLicenseDetails
                /*******prepare post license confirm details *******/
                var ULConfirmDetails = {
                    TransId: ulD.TransId,
                    GatewayTransId: $localStorage.GatewayTransId,
                    itemId: 1,
                    ULPymtId: ulD.Id,
                    ULId: ulD.ULId,
                    IsRenewal: 0,
                    Amount: ulD.Amount,
                    Units: ulD.Units,
                    POSUnits: $localStorage.noOfBTPOSUnits,
                    insupddelflag: 'I',
                    userId: fo.Table[0].userid,
                    foId: fo.Table[0].foid,
                    address: 'Harare, zimbabwe'
                }

                $http({
                    url: '/api/UserLicenses/SaveULConfirmDetails',
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    data: ULConfirmDetails,

                }).success(function (data, status, headers, config) {
                    // alert('Saved successfully');
                    $localStorage.ULConfirmDetails = ULConfirmDetails;
                    $localStorage.ULConfirmDetailsRes = data;
                    window.location.href = "LicenseConfirmation.html";
                    //$scope.ShowConfirmationMssg(data);
                    //   $('#Modal-header-new').modal('hide');
                });
            }).error(function (ata, status, headers, config) {
                alert(ata);

                //    $localStorage.GatewayTransId = 'TEST123';
                //    //do the post payment updates

                //    var fo = $localStorage.foLicenseDetails
                //    /*******prepare post license confirm details *******/
                //    var ULConfirmDetails = {
                //        TransId: ulD.TransId,
                //        GatewayTransId: $localStorage.GatewayTransId,
                //        itemId: 1,
                //        ULPymtId: ulD.Id,
                //        ULId: ulD.ULId,
                //        IsRenewal: 0,
                //        Amount: ulD.Amount,
                //        Units: ulD.Units,
                //        insupddelflag: 'I',
                //        userId: fo.Table[0].userid,
                //        foId: fo.Table[0].foid,
                //        address: 'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA'
                //    }

                //    $http({
                //        url: '/api/UserLicenses/SaveULConfirmDetails',
                //        method: 'POST',
                //        headers: { 'Content-Type': 'application/json' },
                //        data: ULConfirmDetails,

                //    }).success(function (data, status, headers, config) {
                //        alert('Saved successfully');
                //        $localStorage.ULConfirmDetails = ULConfirmDetails;
                //        $localStorage.ULConfirmDetailsRes = data;
                //        $scope.ShowConfirmationMssg(data);
                //    //  $('#Modal-header-new').modal('hide');
                //});


            }).error(function (ata, status, headers, config) {
                alert(ata);
                //insert the failed transaction details
            });
        })
            .error(function (ata, status, headers, config) {
                alert(ata);
                //  $('#Modal-header-new').modal('hide');
            });

        $('#Modal-header-new').modal('hide');
    }

    $scope.FillULConfirmDetails = function () {
        $scope.ulconfirm = $localStorage.ULConfirmDetailsRes[0];
    }

    $scope.processPymt1 = function () {
        $('#Modal-header-new').modal('show');


        //$scope.modal_instance = $uibModal.open({
        //    animation: $scope.animationsEnabled,
        //    backdrop:false,
        //    templateUrl: '/UI/PopupTest.html',
        //    // controller: 'ModalInstanceCtrl',
        //    resolve: {
        //        mssg: function () {
        //            return 'test';
        //        }
        //    }
        //    backdrop: 'static',
        //    keyboard: false,
        //    animation: $scope.animationsEnabled,
        //templateUrl: '/Scripts/angularApp/views/user-modal.html',
        //controller: 'UserModalCtrl',

        //});

        //$http.get('/api/Payments/MakePayment').then(function (response, req) {
        //    $scope.transDetails = response.data;

        //});

        //$http({
        //    url: '/api/Payments/MakePayment',
        //    method: 'GET'
        //}).success(function (data, status, headers, config) {
        //    alert('Saved successfully');
        //    //do the post payment updates

        //}).error(function (ata, status, headers, config) {
        //    alert(ata);
        //    //insert the failed transaction details
        //});

        //$scope.modal_instance.close();
        //$scope.modal_instance.dismiss();

        $('#Modal-header-new').modal('hide');

    }

    $scope.popupTest = function () {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: '/UI/PopupTest.html',
            // controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return 'test';
                }
            }
        });
    }


    $scope.getcheckdetails = function () {

        $scope.c = $localStorage.focheckoutDetails[0];
        $scope.ld = $localStorage.selLicense;
        $scope.ld.amt = $localStorage.UselicensePymtRecord[0].Amount;

        //$http.get('/api/Checkout/getcheckdetails').then(function (response, req) {
        //    $scope.check = response.data;

        //})

    }

    $scope.ShowConfirmationMssg = function (message) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            backdrop: false,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return message;
                }
            }
        });
    }

    $scope.preview = function () {
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'email.html',
            controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return 'test';
                }
            }
        });
    }
});

app.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, $uibModal, mssg) {

    $scope.confirm = mssg[0];
    $scope.ok = function () {
        $uibModalInstance.close('test');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.preview = function () {
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'email.html',
            // controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return (mssg[0] = $scope.confirm);
                }
            }
        });
    }
});