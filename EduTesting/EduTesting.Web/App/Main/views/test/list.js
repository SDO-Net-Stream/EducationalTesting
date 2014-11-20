(function () {
    var controllerId = 'app.views.test.list';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state',
        function ($scope, testService, message, $state) {
            $scope.tests = [];
            testService.getTests().success(function (list) {
                $scope.tests = list;
            });
        }
    ]);
})();
