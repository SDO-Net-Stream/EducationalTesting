(function () {
    var controllerId = 'app.views.account.lostPassword';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.account', 'message', '$state',
        function ($scope, user, accountService, message, $state) {
            var vm = this;
            $scope.user = {};
            $scope.submit = function () {
                if ($scope.form.$valid) {
                    accountService.resetPassword($scope.user).success(function () {
                        message.success("Check your email");
                    });
                }
                return false;
            };
        }
    ]);
})();