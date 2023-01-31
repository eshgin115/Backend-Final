$(document).on("click", '.productmodalshow', function (e) {
    e.preventDefault();
    console.log(e.target.parentElement)
    let aHref = e.target.parentElement.href;
    console.log(aHref);
    $.ajax(
        {
            url: aHref,

            success: function (data) {
                console.log(data)
                $(".modaladdtoquickmodal").html(data);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})