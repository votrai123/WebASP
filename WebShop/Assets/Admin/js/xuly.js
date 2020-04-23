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
selected_id = null;
function setiduser(id) {
    selected_id = id;
}
//delete Category
function deleteCategory() {

    $.post(`/Admin/Category/Delete/${selected_id}`,
    {

    },
    function (data, status) {
        //   alert("Data: " + data + "\nStatus: " + status);
        location.reload();
    });
    // console.log('abcd')
}
//deleteUser
function deleteUser() {

    $.post(`/Admin/User/Delete/${selected_id}`,
    {

    },
    function (data, status) {
        //   alert("Data: " + data + "\nStatus: " + status);
        location.reload();
    });
    // console.log('abcd')
}
//call summernote
$('#Description').summernote({
    placeholder: 'Xin mời nhập mô tả',
    tabsize: 2,
    height: 100
});
$('#Detail').summernote({
    placeholder: 'Xin mời nhập mô tả',
    tabsize: 2,
    height: 100
});
//delete content
function deleteContent() {

    $.post(`/Admin/Content/Delete/${selected_id}`,
    {

    },
    function (data, status) {
        //   alert("Data: " + data + "\nStatus: " + status);
        location.reload();
    });
    // console.log('abcd')
}
//deleteSlide
function deleteSlide() {

    $.post(`/Admin/Slide/Delete/${selected_id}`,
    {

    },
    function (data, status) {
        //   alert("Data: " + data + "\nStatus: " + status);
        location.reload();
    });
    // console.log('abcd')
}
//deleteProduct
function deleteProduct() {

    $.post(`/Admin/Product/Delete/${selected_id}`,
    {

    },
    function (data, status) {
        //   alert("Data: " + data + "\nStatus: " + status);
        location.reload();
    });
    // console.log('abcd')
}
//deleteProductCategory
function deleteProductCategory() {

    $.post(`/Admin/ProductCategory/Delete/${selected_id}`,
        {

        },
        function (data, status) {
            //   alert("Data: " + data + "\nStatus: " + status);
            location.reload();
        });
    // console.log('abcd')
}