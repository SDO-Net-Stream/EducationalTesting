(function () {
    angular.module('app').service('message', function () {
        var htmlEncode = function (text) {
            var element = angular.element("<span>");
            element.text(text);
            return element;
        };
        this.info = function (message, title) {
            if (title)
                window.toastr.info(htmlEncode(message), htmlEncode(title));
            else
                window.toastr.info(htmlEncode(message));
        };
        this.success = function (message, title) {
            if (title)
                window.toastr.success(htmlEncode(message), htmlEncode(title));
            else
                window.toastr.success(htmlEncode(message));
        };
        this.warning = function (message, title) {
            if (title)
                window.toastr.warning(htmlEncode(message), htmlEncode(title));
            else
                window.toastr.warning(htmlEncode(message));
        };
        this.error = function (message, title) {
            if (title)
                window.toastr.error(htmlEncode(message), htmlEncode(title));
            else
                window.toastr.error(htmlEncode(message));
        };
    });
})();