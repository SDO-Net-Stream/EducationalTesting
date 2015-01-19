(function () {
    var controllerId = 'app.views.group.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.group', 'message', '$state', '$stateParams',
        function ($scope, groupService, message, $state, $stateParams) {
            var openGroupParam = $stateParams.group;
            var loadGroups = function () {
                groupService.getGroups({ groupName: $scope.filterGroupName })
                    .success(function (list) {
                        if (openGroupParam) {
                            for (var i = 0; i < list.length; i++)
                                if (list[i].groupId == openGroupParam) {
                                    list[i].$isOpen = true;
                                    break;
                                }
                            openGroupParam = false;
                        }
                        $scope.groups = list;
                    });
            };
            $scope.filter = {};
            var groupTimeout = null;
            $scope.searchGroup = function () {
                if (groupTimeout)
                    clearTimeout(groupTimeout);
                groupTimeout = setTimeout(function () {
                    groupTimeout = null;
                    loadGroups();
                }, 500);
            };
            loadGroups();

            var userTimeout = null;
            $scope.searchUser = function (group) {
                if (userTimeout)
                    clearTimeout(userTimeout);
                userTimeout = setTimeout(function () {
                    userTimeout = null;
                    groupService.getUsers({ userName: group.filterUserName })
                        .success(function (list) {
                            group.filteredUsers = list;
                        });
                }, 500);
            };

            $scope.userInGroup = function (user, group) {
                for (var i = 0; i < group.users.length; i++) {
                    if (group.users[i].userId == user.userId)
                        return true;
                }
                return false;
            };
            var updateGroup = function (group, model) {
                groupService.updateGroup(model).success(function (result) {
                    message.success("Group updated");
                    group.groupName = result.groupName;
                    group.users = result.users;
                });
            };
            var getUpdateModel = function (group) {
                var result = {
                    groupId: group.groupId,
                    groupName: group.groupName,
                    users: []
                };
                for (var i = 0; i < group.users.length; i++)
                    result.users.push(group.users[i].userId);
                return result;
            };

            $scope.addUserToGroup = function (user, group) {
                var model = getUpdateModel(group);
                model.users.push(user.userId);
                updateGroup(group, model);
            };
        }
    ]);
})();
