(function () {
    var controllerId = 'app.views.user.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.user', 'message', '$modal', 'enumConverter',
        function ($scope, userService, message, $modal, enumConverter) {
            $scope.filter = {};
            var loadUsers = function () {
                userService.getUsers($scope.expandFilter ? $scope.filter : { userName: $scope.filter.userName })
                    .success(function (list) {
                        $scope.users = list;
                    });
            };
            var userTimeout = null;
            $scope.searchUser = function () {
                if (userTimeout)
                    clearTimeout(userTimeout);
                userTimeout = setTimeout(function () {
                    userTimeout = null;
                    loadUsers();
                }, 500);
            };
            loadUsers();

            $scope.userRoles = function (user) {
                var roles = [];
                for (var i = 0; i < user.roles.length; i++) {
                    roles.push(enumConverter.userRoleToString(user.roles[i]));
                }
                return roles.join(', ');
            };

            $scope.roleOptions = [
                { l: 'Any', v: null },
                { l: 'User', v: [enumConverter.stringToUserRole('User')] },
                { l: 'Teacher', v: [enumConverter.stringToUserRole('Teacher')] },
                { l: 'Administrator', v: [enumConverter.stringToUserRole('Administrator')] }
            ];
            $scope.$watch('expandFilter', function (v) {
                if (typeof (v) != 'undefined')
                    $scope.searchUser();
            });
        }
    ]);
})();