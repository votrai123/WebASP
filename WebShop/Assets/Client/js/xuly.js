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