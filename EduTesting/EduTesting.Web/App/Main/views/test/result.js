(function () {
    var controllerId = 'app.views.test.result';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal', 'abp.services.app.testResult', '$stateParams',
        function ($scope, testService, message, $state, $modal, testResultService, $stateParams) {
            var loadUsers = function() {
                testResultService.getTestResultsForUsers({ testId: $scope.test.testId, userName: $scope.result.filter.userName })
                    .success(function (list) {
                        $scope.result.users = list;
                    });
            };
            $scope.result = {
                filter: {},
                load: function (test) {
                    loadUsers();
                    
                }
            };
            if ($scope.test) {
                $scope.test.result = $scope.result;
            } else {
                // TODO: load testId from state
                // $scope.test = test;
            }
            var usersTimeout = null;
            $scope.$watch('result.filter.userName', function (filter) {
                if (typeof (filter) != 'undefined') {
                    if (usersTimeout)
                        clearTimeout(usersTimeout);
                    usersTimeout = setTimeout(function () {
                        usersTimeout = null;
                        loadUsers();
                    }, 500);
                }
            });
        }
    ]);
})();
