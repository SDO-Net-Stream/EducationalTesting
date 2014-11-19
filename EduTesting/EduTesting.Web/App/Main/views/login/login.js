(function () {
    var controllerId = 'app.views.login';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.login', '$state', 'message',
        function ($scope, user, loginService, $state, message) {
            var vm = this;
            $scope.user = {
                email: "",
                password: ""
            };
            var signIn = function (info) {
                user.signIn(info);
                message.info('Login successfully completed.', 'Hello, ' + user.name() + '!');
                $state.go('home');
            };
            $scope.submitLogin = function () {
                var promise = loginService.login($scope.user);
                promise.success(function (info) {
                    if (info) {
                        signIn(info);
                    } else {
                        message.error('Email or password is incorrect.', 'Login failed.');
                    }
                });
                return false;
            };
            $scope.ntlmLogin = function () {
                var promise = loginService.ntlmLogin();
                promise.success(function (info) {
                    if (info) {
                        signIn(info);
                    } else {
                        message.error('Login failed.');
                    }
                });
                return false;
            };
        }
    ]);
})();