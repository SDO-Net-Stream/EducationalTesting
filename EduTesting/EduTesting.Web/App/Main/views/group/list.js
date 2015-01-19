(function () {
    var controllerId = 'app.views.group.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.group', 'message', '$state', '$stateParams', '$modal', '$q',
        function ($scope, groupService, message, $state, $stateParams, $modal, $q) {
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
                var defer = $q.defer();
                groupService.updateGroup(model).success(function (result) {
                    message.success("Group updated");
                    group.groupName = result.groupName;
                    group.users = result.users;
                    defer.resolve(result);
                });
                return defer.promise;
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
            $scope.removeUserFromGroup = function (user, group) {
                var model = getUpdateModel(group);
                for (var i = 0; i < model.users.length; i++) 
                    if (model.users[i] == user.userId) {
                        model.users.splice(i, 1);
                        updateGroup(group, model);
                        return;
                    }
            };

            $scope.editGroup = function (group) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.group.list.edit.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = group || {
                            groupName: '',
                            users: []
                        };
                        var model = {
                            isNew: !group,
                            groupName: source.groupName
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            if (group) {
                                source = getUpdateModel(group);
                                source.groupName = model.groupName;
                                updateGroup(group, source).then(function (result) {
                                    $scopeModal.$close(result);
                                });
                            } else {
                                source.groupName = model.groupName;
                                groupService.insertGroup(source).success(function (result) {
                                    message.success("Group created");
                                    $scope.groups.push(result);
                                    $scopeModal.$close(result);
                                });
                            }
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };
            $scope.deleteGroup = function (group) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.group.list.delete.html',
                    controller: function ($scope) {
                        $scope.model = group;
                        $scope.ok = function () {
                            groupService.deleteGroup({ groupId: group.groupId })
                                .success(function () {
                                    message.success("Group '" + group.groupName + "' successfully deleted");
                                    loadGroups();
                                    $scope.$close(group);
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
})();
