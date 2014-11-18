(function () {
    var controllerId = 'app.views.login';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.login', '$location', function ($scope, user, loginService, $location) {
            var vm = this;
            $scope.login = {
                email: "",
                password: ""
            };
            var redirectBack = function () {
                $location.path('/');
            };
            $scope.submitLogin = function () {
                var promise = loginService.login($scope.login);
                promise.success(user.signIn);
                promise.success(redirectBack);
                return false;
            };
            $scope.ntlmLogin = function () {
                var promise = loginService.ntlmLogin();
                promise.success(user.signIn);
                promise.success(redirectBack);
                return false;
            };
            //About logic...
        }
    ]);
})();