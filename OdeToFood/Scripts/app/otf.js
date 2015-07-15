(function ($) {
    var ajaxFormSubmit = function(event) {
        event.preventDefault();
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            method: $form.attr("method"),
            data: $form.serialize()
        }
        $.ajax(options).done(function(data) {
            var $target = $($form.attr("data-otf-target"));
            var $newHtml = $(data);
            $target.html(data);
            $newHtml.effect("highlight");
        });
    }

    var submitAutocompleteForm = function(event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        var $form = $input.parents("form:first");
        $form.submit();
    }
    
    var createAutocomplete = function() {
        var $input = $(this);
        var $options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        }
        $input.autocomplete($options);
    }

    var getPage = function (event) {
        event.preventDefault();
        var $link= $(this);

        var options = {
            url: $link.attr("href"),
            data: $("form").serialize(),
            type: "get"
        }

        $.ajax(options).done(function(data) {
            var target = $link.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });
    }

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-otf-autocomplete]").each(createAutocomplete);
    $(".main-content").on("click", ".pagedList a", getPage);
})(jQuery);