(function () {
    var controllerId = 'app.views.test.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal', 'abp.services.app.testResult',
        function ($scope, testService, message, $state, $modal, testResultService) {
            $scope.tests = [];
            var loadTests = function () {
                testService.getTests().success(function (list) {
                    $scope.tests = list;
                });
            };
            loadTests();
            $scope.create = function () {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.list.add.html',
                    controller: function ($scope) {
                        $scope.model = { testName: "" };
                        $scope.ok = function () {
                            testService
                                .insertTest($scope.model)
                                .success(function (test) {
                                    message.success("Test '" + test.testName + "' successfully created");
                                    $scope.$close(test);
                                    $state.go('test.edit', { test: test.testId });
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
            $scope.delete = function (test) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.list.delete.html',
                    controller: function ($scope) {
                        $scope.model = test;
                        $scope.ok = function () {
                            testService.deleteTest(test)
                                .success(function () {
                                    message.success("Test '" + test.testName + "' successfully deleted");
                                    loadTests();
                                    $scope.$close(test);
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
            $scope.start = function (test) {
                testResultService.startTest({ testId: test.testId }).success(function () {
                    message.success("Test '" + test.testName + "' successfully started");
                    $state.go('test.pass.question', { test: test.testId, question: 1 });
                });
            };
        }
    ]);
})();
