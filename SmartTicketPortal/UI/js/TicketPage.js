var app = angular.module('myApp', ['ngStorage', 'angularFileUpload'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage, fileReader) {

    $scope.GetDetails = function () {

        $scope.BookingId = $localStorage.BookingId;

        if ($scope.BookingId == null) {
            return;
        }

        $http.get('/api/TicketBooking/GetticketDetails?bookingId=' + $scope.BookingId).then(function (response, data) {
            $scope.ticketContent = response.data[0];
            document.getElementById('printableArea').innerHTML = $scope.ticketContent.TicketContent;
        });
    }


    $scope.GetDetails1 = function () {


    }
    $scope.printDiv1 = function (id) {
        var data = document.getElementById(id).innerHTML;
        var myWindow = window.open('', 'my div', 'height=400,width=600');
        myWindow.document.write('<html><head><title>my div</title>');
        /*optional stylesheet*/ //myWindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
        myWindow.document.write('</head><body >');
        myWindow.document.write(data);
        myWindow.document.write('</body></html>');
        myWindow.document.close(); // necessary for IE >= 10

        myWindow.onload = function () { // necessary if the div contain images

            myWindow.focus(); // necessary for IE >= 10
            myWindow.print();
            myWindow.close();
        };
    }
    $scope.printDiv = function (div) {


        var docHead = document.head.outerHTML;
        var printContents = document.getElementById(div).innerHTML;

        //  var pdf = new jsPDF();//'p', 'pt', 'a4');

        var html1 = '<!doctype html><html>' + docHead + '<body onLoad="window.print()">' + printContents + '</body></html>';

       

        var winAttr = "location=yes, statusbar=no, menubar=no, titlebar=no, toolbar=no,dependent=no, width=1265, height=600, resizable=yes, screenX=200, screenY=100, personalbar=no, scrollbars=yes";

        var newWin = window.open("", "_blank", winAttr);
        var writeDoc = newWin.document;
        writeDoc.open();
        writeDoc.write(html1);
        writeDoc.close();
        newWin.focus();
        writeDoc.focus();
    }

    function openPDF(resData, fileName) {
        var ieEDGE = navigator.userAgent.match(/Edge/g);
        var ie = navigator.userAgent.match(/.NET/g); // IE 11+
        var oldIE = navigator.userAgent.match(/MSIE/g);

        if (ie || oldIE || ieEDGE) {
            var blob = b64toBlob(resData, 'application/pdf')
            // window.open(blob, '_blank');
            window.navigator.msSaveBlob(blob, fileName);
           
        }
        else {
            window.open(resData, 'mywin', 'left=200,top=20,width=1000,height=700,toolbar=1,resizable=0');
        }
    }

    function b64toBlob(b64Data, contentType) {
        contentType = contentType || '';
        var sliceSize = 512;
        b64Data = b64Data.replace(/^[^,]+,/, '');
        b64Data = b64Data.replace(/\s/g, '');
        var byteCharacters = window.atob(b64Data);
        var byteArrays = [];

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);

            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            var byteArray = new Uint8Array(byteNumbers);

            byteArrays.push(byteArray);
        }

        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }

    function demoFromHTML() {
        var pdf = new jsPDF('p', 'pt', 'letter')

        
        , source = $('#printableArea')[0]

       
        , specialElementHandlers = {
            
            '#bypassme': function (element, renderer) {
               
                return true
            }
        }

        margins = {
            top: 80,
            bottom: 60,
            left: 40,
            width: 1500
        };
       
        pdf.fromHTML(
            source // HTML string or DOM elem ref.
            , margins.left // x coord
            , margins.top // y coord
            ,
            {
                'width': margins.width // max width of content on PDF
                , 'elementHandlers': specialElementHandlers
            },
            function (dispose) {
                pdf.save('Test.pdf');
            },
            margins
        )
    }

});

