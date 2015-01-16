(function () {
    var controllerId = 'app.views.group.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.group', 'message', '$state',
        function ($scope, groupService, message, $state) {
            var load = function () {
                groupService.getGroups({ groupName: $scope.filter.name })
                    .success(function (list) {
                        $scope.groups = list;
                    });
            };
            $scope.filter = {};
            var nameTimeout = null;
            $scope.$watch('result.filter.name', function (filter) {
                if (typeof (filter) != 'undefined') {
                    if (nameTimeout)
                        clearTimeout(nameTimeout);
                    nameTimeout = setTimeout(function () {
                        nameTimeout = null;
                        load();
                    }, 500);
                }
            });
            load();
        }
    ]);
})();
