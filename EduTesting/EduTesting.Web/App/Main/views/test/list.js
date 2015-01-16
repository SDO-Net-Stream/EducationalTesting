﻿(function () {
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
        }
    ]);
})();
