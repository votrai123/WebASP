$(function () {
    //LoadData();
    $("#btnSave").click(function () {
        //alert("da vo");
        var std = {
            UserName: $("#username").val(),
            Password: $("#password").val(),
            FullName: $("#fullname").val(),
            Country: $("#city").val(),
            StreetAddress: $("#address").val(),
            Phone: $("#phone").val()
        };
        $.ajax({
            type: "POST",
            url: "/Admin/User/Create",
            data: std,
            dataType: "json",
            success: function () {
                $('#exampleModal').modal('hide');
                //LoadData();
            },
            error: function () {
                alert("Error while inserting data");
            }
        });
    });
});

//function LoadData() {
//    $("#tblUser tbody tr").remove();
//    $.ajax({
//        type: 'POST',
//        url: '@Url.Action("getStudent")',
//        dataType: 'json',
//        data: { id: '' },
//        success: function (data) {
//            var items = '';
//            $.each(data, function (i, item) {
//                var rows = "<tr>"
//                + "<td class='prtoducttd'>" + item.studentID + "</td>"
//                + "<td class='prtoducttd'>" + item.studentName + "</td>"
//                + "<td class='prtoducttd'>" + item.studentAddress + "</td>"
//                + "</tr>";
//                $('#tblStudent tbody').append(rows);
//            });
//        },
//        error: function (ex) {
//            var r = jQuery.parseJSON(response.responseText);
//            alert("Message: " + r.Message);
//            alert("StackTrace: " + r.StackTrace);
//            alert("ExceptionType: " + r.ExceptionType);
//        }
//    });
//    return false;
//}