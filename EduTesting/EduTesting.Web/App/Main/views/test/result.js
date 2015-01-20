(function () {
    var controllerId = 'app.views.test.result';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.testResult', 'abp.services.app.group', '$stateParams', 'enumConverter',
        function ($scope, testResultService, groupService, $stateParams, enumConverter) {
            var loadUsers = function() {
                testResultService.getTestResultsForUsers({ testId: $scope.test.testId, userName: $scope.result.filter.userName })
                    .success(function (list) {
                        for (var i = 0; i < list.length; i++) {
                            list[i].testResultStatusCode = enumConverter.testResultStatusToString(list[i].testResultStatus);
                        }
                        $scope.result.users = list;
                    });
            };
            var loadGroups = function () {
                groupService.getGroups({ testId: $scope.test.testId, groupName: $scope.result.filter.groupName })
                    .success(function (list) {
                        $scope.result.groups = list;
                    });
            };
            $scope.result = {
                filter: {},
                load: function (test) {
                    loadUsers();
                    loadGroups();
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

            var groupsTimeout = null;
            $scope.$watch('result.filter.groupName', function (filter) {
                if (typeof (filter) != 'undefined') {
                    if (groupsTimeout)
                        clearTimeout(groupsTimeout);
                    groupsTimeout = setTimeout(function () {
                        groupsTimeout = null;
                        loadGroups();
                    }, 500);
                }
            });
        }
    ]);
})();
