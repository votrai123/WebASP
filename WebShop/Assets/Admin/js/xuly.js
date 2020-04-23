$(document).ready(function () {
    $("#selectcategory").change(function () {
        var role = $(this).val();
        //alert(role);
        $.get("/Admin/Product/GetProductCategory/" + role, function (data) {
        //    //  alert(data);
        //    // alert(role);
        //    $("#users").html(data);
            $("#CategoryID").empty();
            $.each(data, function (index, row) {
                $("#CategoryID").append("<option value='"+row.ID+"'>"+row.Name+"</option>");
            });
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
