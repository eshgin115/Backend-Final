
let baskets = []

const BASKET_PRODUCTS_KEY = "products";
let cardBlock = $(".cart-block")



$(document).on("click", '.add-product-to-basket-btn', function (e) {
    e.preventDefault();
    console.log(e.target.parentElement)
    let aHref = e.target.parentElement.href;
    console.log(aHref);
    $.ajax(
        {
            type: "POST",
            url: aHref,
            success: function (response) {
                console.log(response)
                cardBlock.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})
$(document).on("click", '.add-product-to-basket-btn-modal', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let color = e.target.parentElement.parentElement.previousElementSibling.previousElementSibling.previousElementSibling.children[1]
    let ColorId = color.value;

    let size = e.target.parentElement.parentElement.previousElementSibling.previousElementSibling.children[1]
    let SizeId = size.value;

    let quantity = e.target.parentElement.previousElementSibling.children[0].children[0]

    let Quantity = quantity.value
    console.log(ColorId)
    console.log(SizeId)
    console.log(Quantity)

    console.log(Quantity)
    $.ajax(
        {
            type: "POST",
            url: aHref,

            data: {
                ColorId: ColorId,
                SizeId: SizeId,
                Quantity: Quantity
            },

            success: function (response) {
                console.log(response)
                cardBlock.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})



//#endregion
$(document).on("click", ".product-item_removemy", function (event) {
    event.preventDefault();

    console.log(event.target.parentElement)
    let aHref = event.target.parentElement.href;

    $.ajax({
        url: aHref,
        success: function (response) {
            console.log(response)
            cardBlock.html(response);
        }
    });
})
let cardBlockOrder = $(".cart-block-order")
$(document).on("click", '.add-product-to-basket-btn-order', function (e) {
    e.preventDefault();
    aHref = e.target.parentElement.href;
    console.log(aHref)

    aHrefUpdateBasket = e.target.parentElement.nextElementSibling.href;



    $.ajax(
        {
            type: "POST",
            url: aHref,
            success: function (response) {

                cardBlockOrder.html(response);

                $.ajax(
                    {
                        url: aHrefUpdateBasket,


                        data: {
                            Quantity: 1,
                        },
                        success: function (response) {
                            cardBlock.html(response);
                        }


                    });


            },
            error: function (err) {
                $(".product-details-modal-order").html(err.responseText);

            }

        });

})
$(document).on("change",'.add-size-to-basket-btn-order', function (e) {
    e.preventDefault();
    aHref = e.target.previousElementSibling.href;
    let SizeId =e.target.value
    let Quantity=0



    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                SizeId: SizeId,
                Quantity: Quantity ,
            },
            success: function (response) {

         




            },
            error: function (err) {
                $(".product-details-modal-order").html(err.responseText);

            }

        });

})
$(document).on("change",'.add-color-to-basket-btn-order', function (e) {
    e.preventDefault();
    aHref = e.target.previousElementSibling.href;
    let ColorId = e.target.value
    let Quantity = 0



    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                ColorId: ColorId,
                Quantity: Quantity

            },
        

            success: function (response) {

            },


            
            error: function (err) {
                $(".product-details-modal-order").html(err.responseText);

            }

        });

})



//#endregion
$(document).on("click", ".client-shopping-cart-delete-all", function (event) {

    event.preventDefault();
    aHref = event.target.parentElement.href;
    aHrefUpdateBasket = event.target.parentElement.nextElementSibling.href;
    $.ajax(
        {
            url: aHref,

            success: function (response) {

                cardBlockOrder.html(response);

                $.ajax(
                    {
                        url: aHrefUpdateBasket,

                        success: function (response) {
                        }


                    });


            },
            error: function (err) {
                $(".product-details-modal-order").html(err.responseText);

            }

        });
})


$(document).on("click", ".client-shopping-cart-delete-indivudual1", function (event) {

    event.preventDefault();
    aHref = event.target.parentElement.href;
    aHrefUpdateBasket = event.target.parentElement.nextElementSibling.href;
    $.ajax(
        {
            url: aHref,

            success: function (response) {

                cardBlockOrder.html(response);

                $.ajax(
                    {
                        url: aHrefUpdateBasket,

                        success: function (response) {
                            cardBlock.html(response);
                        }


                    });


            },
            error: function (err) {
                $(".product-details-modal-order").html(err.responseText);

            }

        });
})
