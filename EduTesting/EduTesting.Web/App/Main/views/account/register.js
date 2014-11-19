(function () {
    var controllerId = 'app.views.account.register';
    angular.module('app').controller(controllerId, [
        '$scope', 'user', 'abp.services.app.account', 'message', '$location',
        function ($scope, user, accountService, message, $location) {
            var vm = this;
            $scope.user = {};
            $scope.password_c = "";

            var comparePassword = function () {
                $scope.register.password_c.$setValidity('equal', $scope.user.password == $scope.password_c);
            };

            $scope.$watch('user.password', comparePassword);
            $scope.$watch('password_c', comparePassword);

            $scope.submit = function () {
                var form = this.register;
                if (form.$invalid) {
                    for (var error in form.$error)
                        if (form.$error.hasOwnProperty(error)) {
                            for (var i in form.$error[error])
                                if (form.$error[error].hasOwnProperty(i)) {
                                    form.$error[error][i].$dirty = true;
                                }
                        }
                }
                if (form.$valid) {
                    accountService.registerByEmail($scope.user).success(function () {
                        message.success('User successfully registered');
                        $location.path('/login');
                    });
                }
                return false;
            };
        }
    ]);
})();