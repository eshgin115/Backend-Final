let FilterSlider = $(".FilterSlider")

$(document).on("click", '.search-productByColorId', function (e) {
    e.preventDefault();
    let ehref = e.target.href;
    //console.log(ehref)
    //let aHref = ehref.substr(0, ehref.length - 5);
    //let ColorId = ehref.substr(ehref.length - 1, 1);
    $.ajax(
        {
            url: ehref,

            //data: {
            //    ColorId: ColorId,

            //},

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})

$(document).on("click", '.search-productByTagId', function (e) {
    e.preventDefault();
    let ehref = e.target.href;
    //let aHref = ehref.substr(0, ehref.length - 5);
    //let TagId = ehref.substr(ehref.length - 1, 1);
    $.ajax(
        {
            url: ehref,

            //data: {
            //    TagId: TagId,
            //},

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})

$(document).on("click", '.search-productBySubcategoryId', function (e) {
    e.preventDefault();
    let ehref = e.target.href;
    
    //let aHref = ehref.substr(0, ehref.length - 5);
    //let SubcategoryId = ehref.substr(ehref.length - 1, 1);

    $.ajax(
        {
            url: ehref,

            //data: {
            //    SubcategoryId: SubcategoryId,

            //},

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})






















$(document).keyup(function (event) {
    if (event.keyCode === 13) {
        $(".productBySearchQuery").click();
    }
});


$(document).on("click", '.productBySearchQuery', function (e) {
    e.preventDefault();
    let aHref = document.querySelector(".productBySearchQuery").href;
    let input = document.querySelector(".productBySearchQuery").previousElementSibling;
    let SearchQuery = input.value;
    $.ajax(
        {
            url: aHref,

            data: {
                SearchQuery: SearchQuery,

            },

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})






















$(document).on("change", '.searchProductByPrice', function (e) {
    e.preventDefault();
    let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
    let MinPrice = parseInt(minPrice);
    let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
    let MaxPrice = parseInt(maxPrice);
    let aHref = document.querySelector(".productBySearchQuery").href;
    console.log(MinPrice)
    console.log(MaxPrice)
    console.log(aHref)
    $.ajax(
        {
            url: aHref,

            data: {
                MinPrice: MinPrice,
                MaxPrice: MaxPrice

            },

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})

