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
            var roles = {
                user: enumConverter.stringToUserRole('User'),
                teacher: enumConverter.stringToUserRole('Teacher'),
                administrator: enumConverter.stringToUserRole('Administrator'),
            };
            $scope.roleOptions = [
                { l: 'Any', v: null },
                { l: 'User', v: [roles.user] },
                { l: 'Teacher', v: [roles.teacher] },
                { l: 'Administrator', v: [roles.administrator] }
            ];
            $scope.$watch('expandFilter', function (v) {
                if (typeof (v) != 'undefined')
                    $scope.searchUser();
            });

            $scope.editRoles = function (user) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.user.list.roles.html',
                    controller: ['$scope', function ($scopeModal) {
                        $scopeModal.roleUser = user.roles.indexOf(roles.user) > -1;
                        $scopeModal.roleTeacher = user.roles.indexOf(roles.teacher) > -1;
                        $scopeModal.roleAdministrator = user.roles.indexOf(roles.administrator) > -1;
                        $scopeModal.ok = function () {
                            var entity = angular.copy(user);
                            entity.roles = [];
                            if ($scopeModal.roleUser)
                                entity.roles.push(roles.user);
                            if ($scopeModal.roleTeacher)
                                entity.roles.push(roles.teacher);
                            if ($scopeModal.roleAdministrator)
                                entity.roles.push(roles.administrator);
                            userService.updateUser(entity).success(function () {
                                message.success("User roles have been updated");
                                loadUsers();
                                $scopeModal.$close(entity);
                            });
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };
        }
    ]);
})();