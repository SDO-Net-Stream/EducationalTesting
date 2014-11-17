(function () {
    var controllerId = 'app.views.login';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.login', function ($scope, user, loginService) {
            var vm = this;
            $scope.login = {
                email: "",
                password: ""
            };
            var userLoggedIn = function (loginInfo) {
                if (loginInfo != null) {
                    user.signIn(loginInfo.userName);
                }
            };
            $scope.submitLogin = function () {
                var promise = loginService.login($scope.login);
                promise.success(userLoggedIn);
                return false;
            };
            $scope.ntlmLogin = function () {
                var promise = loginService.ntlmLogin();
                promise.success(userLoggedIn);
                return false;
            };
            //About logic...
        }
    ]);
})();