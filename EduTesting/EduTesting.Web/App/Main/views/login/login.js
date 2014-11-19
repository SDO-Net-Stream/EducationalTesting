(function () {
    var controllerId = 'app.views.login';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.login', '$location', 'message', function ($scope, user, loginService, $location, message) {
            var vm = this;
            $scope.login = {
                email: "",
                password: ""
            };
            var signIn = function (info) {
                user.signIn(info);
                message.info('Login successfully completed.', 'Hello, ' + user.name() + '!');
                $location.path('/');
            };
            $scope.submitLogin = function () {
                var promise = loginService.login($scope.login);
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