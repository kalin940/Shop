$(function () {
    $('.product-cart').on('click', function () {
        let id = $(this).attr("data-id");
        let productName = $(this).attr("data-product");
        let price = $(this).attr("data-price");
        let status = $(this).attr("data-status");
        let brand = $(this).attr("data-brand");
        let quantity = 1;

        //Increase Cart Items Number
        var items = sessionStorage.getItem('cartItems') * 1;
        items++;
        sessionStorage.setItem('cartItems', items);
        $('.shopping-cart').find('small').text(items);

        //Create Item
        let product = {
            id: id,
            product: productName,
            price: price,
            status: status,
            brand: brand,
            quantity: quantity
        };

        console.dir(product);
        let products = [];
        if (sessionStorage.getItem('cartStorage') == null) {    
            products.push(product);
            sessionStorage.setItem('cartStorage', JSON.stringify(products));            
        } else {
            products = JSON.parse(sessionStorage.cartStorage);
            let productFound = false;
            for (let item of products) {
                if (item.id == product.id) {
                    item.quantity++;
                    productFound = true;
                    break;
                }
            }
            if (!productFound) {
                products.push(product);
            }
            sessionStorage.cartStorage = JSON.stringify(products);
        }
       
    });
});