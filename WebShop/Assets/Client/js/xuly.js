$(document).ready(function () {

	var quantitiy = 0;
	$('.quantity-right-plus').click(function (e) {

		// Stop acting like a button
		e.preventDefault();
		// Get the field name
		var quantity = parseInt($('#quantity').val());

		// If is not undefined

		$('#quantity').val(quantity + 1);


		// Increment

	});

	$('.quantity-left-minus').click(function (e) {
		// Stop acting like a button
		e.preventDefault();
		// Get the field name
		var quantity = parseInt($('#quantity').val());

		// If is not undefined

		// Increment
		if (quantity > 0) {
			$('#quantity').val(quantity - 1);
		}
	});

});

// manager cart
var cart = {
	init: function () {
		cart.regEvents();
	},
	regEvents: function () {
		$('#btnContinue').off('click').on('click', function () {
			window.location.href = "/";
		});
		$('#btnPayments').off('click').on('click', function () {
			window.location.href = "/thanh-toan";
		});
		$('#btnUpdate').off('click').on('click', function () {
			var listproduct = $('.ProductQuantity');
			//var listproduct1 = $('#txtSize');
			var cartlist = [];
			$.each(listproduct , function (i,item) {
				cartlist.push({
					Quantity: $(item).val(),
					Product: {
						ID: $(item).data('id')
					}
				});
			});

			$.ajax({
				url: '/Cart/Update',
				data: { CartModel: JSON.stringify(cartlist) },
				dataType: 'json',
				type: 'POST',
				success: function (res) {
					if (res.status == true) {
						window.location.href = "/gio-hang";
					}
				}
			});
		});

		$('#btnDeleteAll').off('click').on('click', function () {
			
			$.ajax({
				url: '/Cart/DeleteAll',
				dataType: 'json',
				type: 'POST',
				success: function (res) {
					if (res.status == true) {
						window.location.href = "/gio-hang";
					}
				}
			});
		});
		
		$('#btnDeleteItem').off('click').on('click', function (e) {
			e.preventDefault();
			$.ajax({
				url: '/Cart/Delete',
				data: {id:$(this).data('id')},
				dataType: 'json',
				type: 'POST',
				success: function (res) {
					if (res.status == true) {
						window.location.href = "/gio-hang";
					}
				}
			});
		});
    }
}
cart.init();

$("#btnPayment").click(function () {
	//alert("da vo");
	
	var Email = $("#email").val();
	var FullName = $("#fullname").val();
	var Country = $("#country").val();
	var StreetAddress = $("#streetaddress").val();
	var Phone = $("#phone").val();
	var Note = $("#note").val();
	
	$.ajax({
		type: "POST",
		url: "/Cart/Payment",
		data: { Email,FullName,Country,StreetAddress,Phone,Note},
		dataType: "json",
		success: function (res) {
			if (res.status == true) {
				window.location.href = "/hoan-thanh";
			}
		},
		error: function () {
				window.location.href = "/that-bai";
		}
	});
});

$("#btnSendFeed").click(function () {
	//alert("da vo");

	var Email = $("#txtEmail").val();
	var FullName = $("#txtName").val();
	var Phone = $("#txtPhone").val();
	var Note = $("#txtContent").val();

	$.ajax({
		type: "POST",
		url: "/Contact/Send",
		data: { Email, FullName, Phone, Note },
		dataType: "json",
		success: function (res) {
			if (res.status == true) {
				window.location.href = "/hoan-thanh";
			}
		},
		error: function () {
			window.location.href = "/that-bai";
		}
	});
});
function initMap() {
	var uluru = { lat: 10.818692, lng: 106.677464 };
	var map = new google.maps.Map(document.getElementById('map'), {
		zoom: 15,
		center: uluru
	});

	var contentString = '';

	var infowindow = new google.maps.InfoWindow({
		content: contentString
	});

	var marker = new google.maps.Marker({
		position: uluru,
		map: map,
		title: 'Address'
	});
	marker.addListener('click', function () {
		infowindow.open(map, marker);
	});
}

google.maps.event.addDomListener(window, 'load', initMap);