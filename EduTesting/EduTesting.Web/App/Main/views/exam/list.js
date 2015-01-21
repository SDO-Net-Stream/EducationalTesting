var inputCounter = 0;
(function () {
    var controllerId = 'app.views.exam.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'message', '$state', '$modal', 'abp.services.app.exam', 'enumConverter',
        function ($scope, message, $state, $modal, examService, enumConverter) {
            var vm = this;
            $scope.tests = [];
            examService.getTests({}).success(function (list) {
                $scope.tests = list;
                for (var i = 0; i < list.length; i++) {
                    if (list[i].testResultStatus != null) {
                        list[i].testResultStatusCode = enumConverter.testResultStatusToString(list[i].testResultStatus);
                    }
                }
            });
            $scope.start = function (test) {
                examService.startTest({ testId: test.testId }).success(function () {
                    message.success("Test '" + test.testName + "' successfully started");
                    $state.go('exam.pass.question', { test: test.testId, question: 1 });
                });
            };
        }
    ]);
})();
