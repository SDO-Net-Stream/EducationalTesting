var inputCounter = 0;
(function () {
    var controllerId = 'app.views.test.exams';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal', 'abp.services.app.testResult', 'enumConverter',
        function ($scope, testService, message, $state, $modal, testResultService, enumConverter) {
            var vm = this;
            $scope.tests = [];
            vm.results = {};
            testService.getTests().success(function (list) {
                $scope.tests = list;
            });
            testResultService.getTestResultsForCurrentUser().success(function (list) {
                vm.results = {};
                for (var i = 0; i < list.length; i++) {
                    list[i].testResultStatusCode = enumConverter.testResultStatusToString(list[i].testResultStatus);
                    vm.results[list[i].testId] = list[i];
                }
            });
            $scope.start = function (test) {
                testResultService.startTest({ testId: test.testId }).success(function () {
                    message.success("Test '" + test.testName + "' successfully started");
                    $state.go('test.pass.question', { test: test.testId, question: 1 });
                });
            };
        }
    ]);
})();
