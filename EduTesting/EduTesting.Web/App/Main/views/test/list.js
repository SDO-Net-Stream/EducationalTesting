(function () {
    var controllerId = 'app.views.test.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', 'user', '$modal', 'enumConverter',
        function ($scope, testService, message, user, $modal, enumConverter) {
            $scope.tests = [];
            var loadTests = function () {
                testService.getTests({}).success(function (list) {
                    for (var i = 0; i < list.length; i++) {
                        list[i].testStatusCode = enumConverter.testStatusToString(list[i].testStatus);
                    }
                    $scope.tests = list;
                });
            };
            $scope.deleteTest = function (test) {
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
            this.resultLoaded = function (test) {
                if (test.isopened && !test.resultLoaded) {
                    test.result.load(test);
                    test.resultLoaded = true;
                }
                return true;
            };
            user.requireRole('Teacher').then(loadTests)
        }
    ]);
})();
