(function () {
    angular.module('app').factory('message', function () {
        return {
            info: function (message, title) {
                window.toastr.info(message, title);
            },
            success: function (message, title) {
                window.toastr.success(message, title);
            },
            warning: function (message, title) {
                window.toastr.warning(message, title);
            },
            error: function (message, title) {
                window.toastr.error(message, title);
            }
        };
    });
})();