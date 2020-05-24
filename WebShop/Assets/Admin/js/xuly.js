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
//deleteAbout
function deleteAbout() {

    $.post(`/Admin/About/Delete/${selected_id}`,
        {

        },
        function (data, status) {
            //   alert("Data: " + data + "\nStatus: " + status);
            location.reload();
        });
    // console.log('abcd')
}
//deleteOrder
function deleteOrder() {

    $.post(`/Admin/Order/Delete/${selected_id}`,
        {

        },
        function (data, status) {
            //   alert("Data: " + data + "\nStatus: " + status);
            location.reload();
        });
    // console.log('abcd')
}
//deleteCommentContent
function deleteCommentContent() {

    $.post(`/Admin/Content/DeleteComment/${selected_id}`,
        {

        },
        function (data, status) {
            //   alert("Data: " + data + "\nStatus: " + status);
            location.reload();
        });
    // console.log('abcd')
}
$(document).ready(function () {
    $('#summernote').summernote({
        toolbar: [

            // This is a Custom Button in a new Toolbar Area
            ['custom', ['examplePlugin']],

            // You can also add Interaction to an existing Toolbar Area
            ['style', ['style', 'examplePlugin']]
        ]
    });
});
$(document).ready(function () {
    $('#summernote').summernote({
        popover: {
            image: [

                // This is a Custom Button in a new Toolbar Area
                ['custom', ['examplePlugin']],
                ['imagesize', ['imageSize100', 'imageSize50', 'imageSize25']],
                ['float', ['floatLeft', 'floatRight', 'floatNone']],
                ['remove', ['removeMedia']]
            ]
        }
    });
});
$.extend(true, $.summernote.lang, {
    'en-US': { /* US English(Default Language) */
        examplePlugin: {
            exampleText: 'Example Text',
            dialogTitle: 'Example Plugin',
            okButton: 'OK'
        }
    }
});
$('#summernote').summernote({
    placeholder: 'Hello stand alone ui',
    tabsize: 2,
    height: 120,
    toolbar: [
        ['style', ['style']],
        ['font', ['bold', 'underline', 'clear']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['table', ['table']],
        ['insert', ['link', 'picture', 'video']],
        ['view', ['fullscreen', 'codeview', 'help']]
    ]
});