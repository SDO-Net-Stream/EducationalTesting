(function() {
    var controllerId = 'app.views.home';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.test', function ($scope, testService) {
            var vm = this;
            //Home logic...
        }
    ]);
})();