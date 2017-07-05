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


        demoFromHTML();
        $scope.dt = new Date();
      //  get the booking id and get the details from db
      //  if obtained from db then take it otherwise retive from session
        $scope.BookingId = $localStorage.BookingId;

        $scope.srcStage = $localStorage.src.Name;
        $scope.destStage = $localStorage.dest.Name;

        $scope.onwarddetails = $localStorage.onwarddetails;

        $scope.source = $localStorage.srcId;
        $scope.destination = $localStorage.destId;
        $scope.details = $localStorage.book;
        $scope.firstName = $scope.details.No_Seats;
        $scope.NoofSeats = $scope.details.NoofSeats;
        $scope.JourneyType = $scope.details.JourneyType;

        var docHead = document.head.outerHTML;
        var printContents = document.getElementById('printableArea').outerHTML;

        // var pdf = new jsPDF('p', 'pt', 'letter');

        var html1 = '<!doctype html><html>' + docHead + '<body onLoad="window.print()">' + printContents + '</body></html>';

        var BookedTicketDetails = {
            BookingId: $scope.BookingId,
            TicketNo: $scope.onwarddetails.TicketNo,
            TransId: 'TR0001',//$scope.onwarddetails.TransId,
            EmailId: $scope.onwarddetails.EmailId,
            MobileNo: $scope.onwarddetails.MobileNo,
            insupddelflag:'I',
            TicketContent: html1
        }

        var req = {
            method: 'POST',
            url: '/api/TicketBooking/SaveBookedTicket',
            data: BookedTicketDetails
        }

        $http(req).then(function (res) {

        });
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

        //  var html = '<html>' + docHead + '<body>' + printContents + '</body></html>';

        //html2pdf(html, pdf, function (pdf) {
        //    var content = pdf.output('blob');
        //    //pdf.output('dataurlnewwindow');
        //    //pdf.save('ticket.pdf')

        //    fileReader.readAsDataUrl(content, $scope, 4).then(function (result) {

        //        var docContent = result;
        //        openPDF(docContent, "test.pdf");
        //    });
        //});

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
            //window.open(resData, '_blank');
            //  var a = document.createElement("a");
            //  document.body.appendChild(a);
            //  a.style = "display: none";
            //  a.href = resData;
            //  a.download = fileName;
            ////  a.onclick = "window.open(" + fileURL + ", 'mywin','left=200,top=20,width=1000,height=800,toolbar=1,resizable=0')";
            //  a.click(); 

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

        // source can be HTML-formatted string, or a reference
        // to an actual DOM element from which the text will be scraped.
        , source = $('#printableArea')[0]

        // we support special element handlers. Register them with jQuery-style 
        // ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
        // There is no support for any other type of selectors 
        // (class, of compound) at this time.
        , specialElementHandlers = {
            // element with id of "bypass" - jQuery style selector
            '#bypassme': function (element, renderer) {
                // true = "handled elsewhere, bypass text extraction"
                return true
            }
        }

        margins = {
            top: 80,
            bottom: 60,
            left: 40,
            width: 1500
        };
        // all coords and widths are in jsPDF instance's declared units
        // 'inches' in this case
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
                // dispose: object with X, Y of the last line add to the PDF 
                //          this allow the insertion of new lines after html
                pdf.save('Test.pdf');
            },
            margins
        )
    }

});

