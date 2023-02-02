let FilterBlog = $(".Filterblog")


$(document).on("click", '.search-blogByTagId', function (e) {
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
                FilterBlog.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})





$(document).keyup(function (event) {
    if (event.keyCode === 13) {
        $(".blogBySearchQuery").click();
    }
});


$(document).on("click", '.blogBySearchQuery', function (e) {
    e.preventDefault();
    let aHref = document.querySelector(".blogBySearchQuery").href;
    let input = document.querySelector(".blogBySearchQuery").previousElementSibling;
    let SearchQuery = input.value;
    $.ajax(
        {
            url: aHref,

            data: {
                SearchQuery: SearchQuery,

            },

            success: function (response) {
                FilterBlog.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})