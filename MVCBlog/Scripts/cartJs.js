$(function () {

    let total = 0;
    let emty = false;
    if (sessionStorage.getItem('cartStorage') == null) {
        let emtyTable = $('<tr>').append($('<td>').html('<strong>Your cart is empty</strong>'));
        $('#cartTable').append(emtyTable);
        emty = true;
    } else {
        let products = JSON.parse(sessionStorage.cartStorage);
        for (let item of products) {
            let row = `<tr>
                        <td class="col-sm-8 col-md-6">
                            <div class="media">
                                <a class="thumbnail pull-left" href="#"> <img class="media-object" src="http://icons.iconarchive.com/icons/custom-icon-design/flatastic-2/72/product-icon.png" style="width: 72px; height: 72px;"> </a>
                                <div class="media-body">
                                    <h4 class="media-heading"><a href="#">${item.product}</a></h4>
                                    <h5 class="media-heading"> by <a href="#">${item.brand}</a></h5>
                                    <span>Status: </span><span class="text-success"><strong>${item.status}</strong></span>
                                </div>
                            </div>
                        </td>
                        <td class="col-sm-1 col-md-1" style="text-align: center">
                            <input type="number" class="quantity form-control text-center" data-id="${item.id}" value="${item.quantity}">
                        </td>
                        <td class="col-sm-1 col-md-1 text-center"><strong class="price">$${item.price}</strong></td>
                        <td class="col-sm-1 col-md-1 text-center"><strong class="totalprice">$${(item.price * 1 * item.quantity).toFixed(2)}</strong></td>
                        <td class="col-sm-1 col-md-1">
                            <button type="button" class="remove btn btn-danger" data-id="${item.id}">
                                <span class="glyphicon glyphicon-remove"></span> Remove
                            </button>
                        </td>
                    </tr>`;
            total += item.price * 1 * item.quantity;
            $('#cartTable').append(row);
        }
    }

    let tableEnd = `<tr>
                        <td>   </td>
                        <td>   </td>
                        <td>   </td>
                        <td><h3>Total</h3></td>
                        <td id="total" class="text-right"><h3><strong>${total.toFixed(2)}</strong></h3></td>
                    </tr>
                    <tr>
                        <td>   </td>
                        <td>   </td>
                        <td>   </td>
                        <td>
                            <button id="continue-shopping" type="button" class="btn btn-default">
                                <span class="glyphicon glyphicon-shopping-cart"></span> Continue Shopping
                            </button>
                        </td>
                        <td>
                            <button id="checkout" type="button" class="btn btn-success">
                                Checkout <span class="glyphicon glyphicon-play"></span>
                            </button>
                        </td>
                    </tr>`;

    $('#cartTable').append(tableEnd);


    $('.quantity').on('change', function () {
        let td=$(this).context.parentNode;
        let tr = $(td).context.parentNode;
        let cartTotalItems = 0;
        let newQuantity = $(this).val() * 1;
        if (newQuantity >= 0) {
            let id = $(this).attr("data-id");
            let products = JSON.parse(sessionStorage.cartStorage);
            for (let item of products) {
                if (item.id == id) {
                    let difference = newQuantity - item.quantity;
                    item.quantity = newQuantity;
                    if (difference > 0) {
                        total += item.price * 1;
                    }
                    if (difference < 0) {
                        total -= item.price * 1;
                    }
                    total.toFixed(2);
                    $('#total').find('strong').text(total.toFixed(2));
                    let totalprice = item.quantity * item.price * 1;
                    totalprice.toFixed(2);
                    $(tr).find('.totalprice').text("$" + totalprice.toFixed(2));
                }
                cartTotalItems += item.quantity * 1;
            }
            sessionStorage.cartStorage = JSON.stringify(products);
            $('.shopping-cart').find('small').text(cartTotalItems);
            sessionStorage.cartItems=cartTotalItems;
        } else {
            $(this).val(0); 
        }
        
    });

    $('.remove').click(function () {
        let id = $(this).attr("data-id");
        let counter = 0;
        let products = JSON.parse(sessionStorage.cartStorage);
        for (let item of products) {
            if (item.id == id) {
                let totalItem = 1 * item.price * item.quantity;
                total -= totalItem * 1;
                total.toFixed(2);
                let cartCount = (sessionStorage.cartItems * 1) - item.quantity;
                sessionStorage.cartItems = cartCount;
                $('.shopping-cart').find('small').text(cartCount);
                $('#total').find('strong').text(total.toFixed(2));
                products.splice(counter, 1);
                break;
            }
            counter++;
        }
        sessionStorage.cartStorage = JSON.stringify(products);
        $(this).closest("tr").remove();
    });


    $('#continue-shopping').click(function () {
        window.location.replace('https://localhost:44321/Products/Products');
    });

    $('#checkout').click(function () {
        let products = JSON.parse(sessionStorage.cartStorage)
        if (products==[] || products==null || products==undefined) {
            console.log("Please add items to cart before you check out")
        } else {
            var model = new Object();
            model.Keyword = sessionStorage.cartStorage;
            $.ajax({
                type: "POST",
                url: "https://localhost:44321/Home/PostOrder",   //ENTER VALID URL
                contentType: "application/json",
                data: JSON.stringify({ keyword: model.Keyword }),
                success: function (data) {
                    console.log(data);
                   
                },
                error: function (error) {
                    console.log("fail");
                }
            });
        }
    });
});