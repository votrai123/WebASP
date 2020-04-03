$(function () {
    //LoadData();
    $("#btnSave").click(function () {
        //alert("da vo");
        var std = {
            UserName: $("#username").val(),
            Password: $("#password").val(),
            Email: $("#email").val(),
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
