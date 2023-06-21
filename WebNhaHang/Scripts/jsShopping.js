

$(document).ready(function (){
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quatity = parseInt(tQuantity);
        }
        
        /*alert(id + " " + quatity);*/
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quatity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.Count);
                    
                }
            }
        });    
    });
    $('body').on('click', '.btnUpdate', function () {

        var id = $(this).data('id');
      
            var id = $(this).data("id");
            var quantity = $('#Quantity_' + id).val();
            Update(id, quantity);
        

    });

    $('body').on('click', '.btnDeleteAll', function () {

        var id = $(this).data('id');
        var conf = confirm('Do you want to delete all this cart?');
        if (conf === true) {
            DeleteAll();
        }

    });

    $('body').on('click', '.btnDelete', function () {
        
        var id = $(this).data('id');
        var conf = confirm('Do you want to delete this product?');
        if (conf === true) {
            $.ajax({
            url: '/shoppingcart/Delete',
            type: 'POST',
            data: { id: id },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.Count);
                    $('#trow_' + id).remove();
                    LoadCart();
                }
            }
        });
        }
        
    });

});

function ShowCount() {
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);

        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/ShoppingCart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            } 

        }
    });
}
function Update(id,quantity) {
    $.ajax({
        url: '/ShoppingCart/Update',
        type: 'POST',
        data: {id:id,quantity,quantity},
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            }

        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/ShoppingCart/Partial_item_Cart',
        type: 'POST',
        success: function (rs) {
            $('#load_data').html(rs);

        }
    });
}