﻿(function () {
    var controllerId = 'app.views.account.resetPassword';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.account', 'message', '$state', '$stateParams',
        function ($scope, user, accountService, message, $state, $stateParams) {
            var vm = this;
            $scope.model = {
                token: $stateParams.token
            };
            $scope.password_c = "";
            var comparePassword = function () {
                $scope.form.password_c.$setValidity('equal', $scope.model.password == $scope.password_c);
            };

            $scope.$watch('model.password', comparePassword);
            $scope.$watch('password_c', comparePassword);


            $scope.submit = function () {
                if ($scope.form.$valid) {
                    accountService.resetPasswordConfirm($scope.model).success(function () {
                        message.success("Password successfully changed");
                        $state.go('login');
                    });
                }
                return false;
            };
        }
    ]);
})();