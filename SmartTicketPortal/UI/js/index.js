
var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {

    $scope.selectedOp = 0;

    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }

    $scope.carouselImages = [{ "ID": 1, "Name": "TRAVEL WITH INTERBUS", "Caption": "Every Journey Matters....", "Path": "UI/Images/promos/11.jpg" }
        , { "ID": 2, "Name": "Customer satisfaction", "Caption": "The comfort and convienience of travelling with INTERBUS", "Path": "/UI/Images/promos/12.png" }
        , { "ID": 3, "Name": "Online Ticket Booking", "Caption": "Automated ticketing increases performance and convienience", "Path": "/UI/Images/promos/13.jpg" }
        , { "ID": 4, "Name": "Hassel free travel", "Caption": "Get online tickets to make the journey hassel free", "Path": "/UI/Images/promos/14.png" }
        , { "ID": 5, "Name": "Extensive coverage", "Caption": "Wide network taking you to various destinations", "Path": "/UI/Images/promos/2.png" }
    ];
    $scope.triptype = "oneway";

    //$scope.timing = "Now";

    //$scope.ChangeTravelType = function (travelTime) {
    //    $scope.timing = (travelTime == 0) ? "Now" : "Later";
    //}

    $scope.RadioChange = function (s) {
        $scope.triptype = s;
    };

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

    $scope.GetServices = function () {
        if ($scope.S == null) {
            // alert('Please select source.');

            $scope.showDialog('Please select source.');
            return;
        }

        if ($scope.D == null) {
            $scope.showDialog('Please select destination.');
            return;
        }

        $localStorage.src = $scope.S;
        $localStorage.dest = $scope.D;

        //$rootscope.src = $scope.RS;
        //$rootscope.dest = $scope.RD;
        // $localStorage.timing = ($scope.timing == 'Now') ? Date() : $scope.timing;
        $localStorage.triptype = $scope.triptype;
        window.location.href = "UI/booking.html";
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
                    window.location.href = "UI/BookedTicketHistory.html";
                }
                else {
                    window.location.href = "UI/UserProfile.html";
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

    //$scope.GotToLicensePage = function (t) {
    //    $localStorage.licenseId = t;
    //    window.location.href = "UI/LicensePage.html";
    //}

    $scope.RecentJourneyClick = function () {
        if ($localStorage.uname) {
            window.location.href = "UI/BookedTicketHistory.html";
        }
        else {
            $scope.selectedOp = 1;
        }

    }


    $scope.showDialog = function (message) {
        //alert(message);
        //var modalInstance = $uibModal.open({
        //    animation: $scope.animationsEnabled,
        //    templateUrl: 'statusPopup.html',
        //    controller: 'ModalInstanceCtrl',
        //    resolve: {
        //        mssg: function () {
        //            return message;
        //        }
        //    }
        //});
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

function fun() {
    if (document.getElementById("ddlBusType").value == 2) {//if Booking Type
        window.location.href = "booking.html";

    } else if (document.getElementById("ddlBusType").value == 3) {//if Hiring Type

        window.location.href = "vehicleavailability.html";
    }
}


