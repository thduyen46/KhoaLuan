(function ($) {
    "use strict";

    var input = document.querySelectorAll('input.tags')
    if (input != null && input.length > 0) {
        input.forEach(x => {
            var tagify = new Tagify(x, {
                dropdown: {
                    enabled: 0
                },
            })
            tagify.on('change', console.log)
        })
    }
})(jQuery);
