(function () {
    var controllerId = 'app.views.test.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal',
        function ($scope, testService, message, $state, $modal) {
            $scope.tests = [];
            testService.getTests().success(function (list) {
                $scope.tests = list;
            });
            $scope.create = function () {
                var modalInstance = $modal.open({
                    templateUrl: 'app.views.test.list.add.html',
                    controller: function ($scope) {
                        $scope.model = { name: "" };
                        $scope.ok = function () {
                            testService
                                .insertTest({ testName: $scope.model.name })
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
        }
    ]);
    app.controller('app.views.test.list.add', [
        '$scope',
        function ($scope) {
            $scope.model = { name: "" };
            $scope.ok = function () {
                $scope.$close($scope.model);
            };
            $scope.cancel = function () {
                $scope.$dismiss
            };
        }
    ]);
})();
