(function($) {
    $("#restaurantsearch").submit(function (evt) {
        var self = $(this);
        evt.preventDefault();
        $.ajax({
            url: "/",
            method: "GET",
            dataType: "html",
            data: self.serialize(),
            success: function(data) {
                $("#restaurantList").html(data);
            }
        });
    });
})(jQuery);