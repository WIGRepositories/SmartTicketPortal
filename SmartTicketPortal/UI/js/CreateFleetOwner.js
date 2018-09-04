// JavaScript source code
var app = angular.module('myApp', ['ngStorage', 'ui.bootstrap', 'angularFileUpload'])

app.directive('file-input', function ($parse) {
    return {
        restrict: "EA",
        template: "<input type='file' />",
        replace: true,
        link: function (scope, element, attrs) {

            var modelGet = $parse(attrs.fileInput);
            var modelSet = modelGet.assign;
            var onChange = $parse(attrs.onChange);

            var updateModel = function () {
                scope.$apply(function () {
                    modelSet(scope, element[0].files[0]);
                    onChange(scope);
                });
            };

            element.bind('change', updateModel);
        }
    };
});

app.directive("ngFileSelect", function () {

    return {

        link: function ($scope, el) {

            el.on('click', function () {

                this.value = '';

            });

            el.bind("change", function (e) {

                $scope.file = (e.srcElement || e.target).files[0];



                var allowed = ["jpeg", "png", "gif", "jpg"];

                var found = false;

                var img;

                img = new Image();

                allowed.forEach(function (extension) {

                    if ($scope.file.type.match('image/' + extension)) {

                        found = true;

                    }

                });

                if (!found) {

                    alert('file type should be .jpeg, .png, .jpg, .gif');

                    return;

                }

                img.onload = function () {

                    var dimension = $scope.selectedImageOption.split(" ");

                    if (dimension[0] == this.width && dimension[2] == this.height) {

                        allowed.forEach(function (extension) {

                            if ($scope.file.type.match('image/' + extension)) {

                                found = true;

                            }

                        });

                        if (found) {

                            if ($scope.file.size <= 1048576) {

                                $scope.getFile();

                            } else {

                                alert('file size should not be grater then 1 mb.');

                            }

                        } else {

                            alert('file type should be .jpeg, .png, .jpg, .gif');

                        }

                    } else {

                        alert('selected image dimension is not equal to size drop down.');

                    }

                };

                //  img.src = _URL.createObjectURL($scope.file);



            });

        }

    };

});

app.directive("ngFileSelect1", function () {

    return {

        link: function ($scope, el) {

            el.on('click', function () {

                this.value = '';

            });

            el.bind("change", function (e) {

                $scope.file1 = (e.srcElement || e.target).files[0];



                var allowed = ["jpeg", "png", "gif", "jpg"];

                var found = false;

                var img;

                img = new Image();

                allowed.forEach(function (extension) {

                    if ($scope.file1.type.match('image/' + extension)) {

                        found = true;

                    }

                });

                if (!found) {

                    alert('file type should be .jpeg, .png, .jpg, .gif');

                    return;

                }

                img.onload = function () {

                    var dimension = $scope.selectedImageOption.split(" ");

                    if (dimension[0] == this.width && dimension[2] == this.height) {

                        allowed.forEach(function (extension) {

                            if ($scope.file1.type.match('image/' + extension)) {

                                found = true;

                            }

                        });

                        if (found) {

                            if ($scope.file1.size <= 1048576) {

                                $scope.getFile1();

                            } else {

                                alert('file size should not be grater then 1 mb.');

                            }

                        } else {

                            alert('file type should be .jpeg, .png, .jpg, .gif');

                        }

                    } else {

                        alert('selected image dimension is not equal to size drop down.');

                    }

                };

                //  img.src = _URL.createObjectURL($scope.file);



            });

        }

    };

});

var ctrl = app.controller('myCtrl', function ($scope, $http, $uibModal, $localStorage, $upload, fileReader) {
    $scope.selectedOp = 0;
    if ($localStorage.uname) {
        $scope.username = $localStorage.uname;
    }

    $scope.GetCountry = function () {
        $http.get('/api/Country/GetCountry?active=1').then(function (response, req) {
            $scope.Country = response.data;
        });
    }


    $scope.save = function (FleetOwnerRequest1, flag) {
        if (FleetOwnerRequest1 == null) {
            alert('Please enter FirstName.');
            return;
        }
        if (FleetOwnerRequest1.FirstName == null) {
            alert('Please enter FirstName.');
            return;
        }
        if (FleetOwnerRequest1.LastName == null) {
            alert('Please enter LastName.');
            return;
        }
        if (FleetOwnerRequest1.CompanyName == null) {
            alert('Please enter CompanyName.');
            return;
        }
        if (FleetOwnerRequest1.PhoneNo == null) {
            alert('Please enter PhoneNo.');
            return;
        }
        var fleetOwnerRequest = {
            //user details            
            FirstName: FleetOwnerRequest1.FirstName,
            LastName: FleetOwnerRequest1.LastName,
            MiddleName: FleetOwnerRequest1.MiddleName,
            EmailAddress: FleetOwnerRequest1.EmailAddress,
            PhoneNo: FleetOwnerRequest1.PhoneNo,
            AltPhoneNo: FleetOwnerRequest1.AltPhoneNo,
            Gender: FleetOwnerRequest1.Gender,
            Address: FleetOwnerRequest1.Address,


            userPhoto: $scope.imageUserSrc,
            //company details
            CompanyName: FleetOwnerRequest1.CompanyName,
            CmpEmailAddress: FleetOwnerRequest1.CmpEmailAddress,
            CmpAddress: FleetOwnerRequest1.CmpAddress,
            CmpAltAddress: FleetOwnerRequest1.CmpAltAddress,
            Title: FleetOwnerRequest1.CmpTitle,
            CmpCaption: FleetOwnerRequest1.CmpCaption,
            FleetSize: FleetOwnerRequest1.FleetSize,
            StaffSize: FleetOwnerRequest1.StaffSize,
            Country: FleetOwnerRequest1.Country.Id,
            state: FleetOwnerRequest1.state,

            Code: $scope.GetUID(),
            CmpFax: FleetOwnerRequest1.CmpFax,
            CmpPhoneNo: FleetOwnerRequest1.CmpPhoneNo,
            CmpAltPhoneNo: FleetOwnerRequest1.CmpAltPhoneNo,

           

            ZipCode: FleetOwnerRequest1.ZipCode,
            CmpLogo: $scope.imageSrc,

            //General details        
            CurrentSystemInUse: FleetOwnerRequest1.CurrentSystemInUse,
            SentNewProductsEmails: 1,
            howdidyouhearaboutus: FleetOwnerRequest1.howdidyouhearaboutus,
            Agreetotermsandconditions: 1,

            insupdflag: 'I',


        }
        var req = {
            url: '/api/FleetOwnerLicense/CreateNewFOR',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: fleetOwnerRequest
        }

        $http(req)
            .success(function (data, status, headers, config) {
                //if (data[0]['status'] == "0") {
                //    alert(data[0]['details']);
                //    return;
                //}

                $localStorage.NewFOcode = data[0].FleetOwnerCode;
                $localStorage.FOemailid = FleetOwnerRequest1.EmailAddress;
                // $scope.showDialog('saved successfully. The fleet owner code is ' + data[0].FleetOwnerCode + '.\n please use the code to buy license.\n The same has been sent to the given e-mailid:' + FleetOwnerRequest1.EmailAddress+'.',0);


                window.location.href = "FOConfirmation.html";
            }).error(function (ata, status, headers, config) {
                $scope.showDialog(ata.ExceptionMessage, 1);
            });

        $scope.clearFleetOwnerRequest1 = function () {
            $scope.FleetOwnerRequest1 = null;
        };
    }

    $scope.FillFOConfirmDetails = function () {
        $scope.NewFOCode = $localStorage.NewFOcode;
        $scope.FOemailid = $localStorage.FOemailid;
    }

    $scope.showDialog = function (message, status) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'statusPopup.html',
            controller: 'ModalInstanceCtrl',
            resolve: {
                mssg: function () {
                    return message;
                },
                status: function () {
                    return status;
                }
            }
        });
    }


    $scope.filterValue = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            $event.preventDefault();
        }
    };

    $scope.GetUID = function () {

        var date = new Date();
        var components = [
            date.getHours(),
            date.getMinutes(),
            date.getSeconds()
        ];

        var id = components.join("");
        return 'CMP' + id;
    }

    $scope.UploadUserImg = function () {
        var fileinput = document.getElementById('fileInput');
        fileinput.click();

        //  
        //if ($scope.file == null)
        //{ $scope.file = fileinput.files[0]; }
        //fileReader.readAsDataUrl($scope.file, $scope).then(function (result) { $scope.imageSrc = result; });
        //fileReader.onLoad($scope.file, $scope).then(function (result) { $scope.imageSrc = result; });
    };

    $scope.onFileSelectUser = function () {
        fileReader.readAsDataUrl($scope.file1, $scope).then(function (result) { $scope.imageUserSrc = result; });
    }

    $scope.clearUserImg = function () {
        $scope.imageUserSrc = null;
        //document.getElementById('cmpLogo').src = "";
        document.getElementById('userNewLogo').src = "";
    }

    $scope.UploadImg = function () {
        var fileinput = document.getElementById('cfileInput');
        fileinput.click();

        //  
        //if ($scope.file == null)
        //{ $scope.file = fileinput.files[0]; }
        //fileReader.readAsDataUrl($scope.file, $scope).then(function (result) { $scope.imageSrc = result; });
        //fileReader.onLoad($scope.file, $scope).then(function (result) { $scope.imageSrc = result; });
    };

    $scope.onFileSelect = function () {
        fileReader.readAsDataUrl($scope.file, $scope).then(function (result) { $scope.imageSrc = result; });
    }

    $scope.clearImg = function () {
        $scope.imageSrc = null;
        // document.getElementById('cmpLogo').src = "";
        document.getElementById('cmpNewLogo').src = "";
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

    $scope.getFile = function () {

        var dimension = $scope.selectedImageOption.split(" ");

        fileReader.readAsDataUrl($scope.file, $scope)

                      .then(function (result) {

                          $scope.imagePreview = true;

                          $scope.upladButtonDivErrorFlag = false;

                          $('#uploadButtonDiv').css('border-color', '#999');

                          $scope.imageSrc = result;

                          var data = {

                              "height": dimension[2],

                              "weight": dimension[0],

                              "imageBean": {

                                  "imgData": result,

                                  "imgName": $scope.file.name

                              }

                          }

                          $scope.imagePreviewDataObject = data;

                      });

    }

    $scope.getFile1 = function () {

        var dimension = $scope.selectedImageOption.split(" ");

        fileReader.readAsDataUrl($scope.file, $scope)

                      .then(function (result) {

                          $scope.imagePreview = true;

                          $scope.upladButtonDivErrorFlag = false;

                          $('#uploadButtonDiv').css('border-color', '#999');

                          $scope.imageUserSrc = result;

                          var data = {

                              "height": dimension[2],

                              "weight": dimension[0],

                              "imageBean": {

                                  "imgData": result,

                                  "imgName": $scope.file.name

                              }

                          }

                          $scope.imagePreviewDataObject = data;

                      });

    }
});

app.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, mssg, status) {

    $scope.mssg = mssg;
    $scope.status = status;
    $scope.ok = function () {
        $uibModalInstance.close('test');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

});










