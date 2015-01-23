(function () {
    var app = angular.module('app');
    var username = null;
    var roles = [];
    var enumConverter = null;

    app.service('user', [
        '$q', 'enumConverter', '$state',
        function ($q, enumConverter, $state) {
            var loaded = $q.defer();
            this.signIn = function (loginInfo) {
                if (loginInfo != null) {
                    username = loginInfo.userName;
                    roles = [];
                    if (loginInfo.userRoles) {
                        for (var i = 0; i < loginInfo.userRoles.length; i++)
                            roles.push(enumConverter.userRoleToString(loginInfo.userRoles[i]));
                    }
                }
                loaded.resolve();
            };
            this.isAuthenticated = function () {
                return !!username;
            };
            this.name = function () {
                return username;
            },
            this.roles = function () {
                var result = {};
                for (var i = 0; i < roles.length; i++)
                    result[roles[i].toLowerCase()] = true;
                return result;
            };
            this.requireRole = function (roleName) {
                var defer = $q.defer();
                loaded.promise.then(function () {
                    if (roles.indexOf(roleName) == -1) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                }, function () {
                    defer.reject();
                });
                return defer.promise;
            };
        }
    ]);

    app.value('_user', {
        signIn: function (loginInfo) {
            if (!enumConverter) {
                enumConverter = angular.injector(['app']).get('enumConverter');
            }
            if (loginInfo != null) {
                username = loginInfo.userName;
                roles = [];
                if (loginInfo.userRoles) {
                    for (var i = 0; i < loginInfo.userRoles.length; i++)
                        roles.push(enumConverter.userRoleToString(loginInfo.userRoles[i]));
                }
            }
        },
        isAuthenticated: function () {
            return !!username;
        },
        name: function () {
            return username;
        },
        roles: function () {
            var result = {};
            for (var i = 0; i < roles.length; i++)
                result[roles[i].toLowerCase()] = true;
            return result;
        },
    });
    app.run(['user', 'abp.services.app.login', function (user, loginService) {
        loginService.getUserInfo()
            .success(user.signIn)
            .error(function () { user.signIn(null); });
    }]);
})();