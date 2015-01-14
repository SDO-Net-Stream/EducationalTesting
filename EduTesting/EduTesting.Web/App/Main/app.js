(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');
            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in EduTestingNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in EduTestingNavigationProvider
                })
                .state('login', {
                    url: '/login',
                    templateUrl: '/App/Main/views/login/login.cshtml',
                })
                .state('register', {
                    url: '/account/register',
                    templateUrl: '/App/Main/views/account/register.cshtml',
                })
                .state('lostPassword', {
                    url: '/account/lostPassword',
                    templateUrl: '/App/Main/views/account/lostPassword.cshtml',
                })
                .state('resetPassword', {
                    url: '/account/resetPassword/:token',
                    templateUrl: '/App/Main/views/account/resetPassword.cshtml',
                })

                .state('test', {
                    abstract: true,
                    url: '/test',
                    template: '<ui-view/>'
                })
                .state('test.list', {
                    url: '/list',
                    templateUrl: '/App/Main/views/test/list.cshtml',
                })
                .state('test.edit', {
                    url: '/:test/edit',
                    templateUrl: '/App/Main/views/test/edit.cshtml',
                })


                .state('test.available', {
                    url: '/available',
                    templateUrl: '/App/Main/views/test/available.cshtml',
                })
                .state('test.pass', { // answering test
                    url: '/:test/pass',
                    abstract: true,
                    resolve: {
                        testResult: [
                            'abp.services.app.testResult', '$stateParams', '$q', '$state',
                            function (resultService, $stateParams, $q, $state) {
                                var result = resultService.getActiveUserTestResult({ testId: $stateParams.test });
                                var defer = $q.defer();
                                result.success(function (testResult) { defer.resolve(testResult); });
                                result.error(function () {
                                    $state.go('test.list');
                                    defer.reject();
                                });
                                return defer.promise;
                            }
                        ]
                    },
                    template: '<ui-view/>'
                })
                .state('test.pass.question', {
                    url: '/:question',
                    templateUrl: '/App/Main/views/test/pass.cshtml',
                    controller: 'app.views.test.pass'
                })
                .state('test.result', { // test results
                    url: '/:test/result',
                })
                .state('test.result.details', {
                    url: '/:user'
                })

            ;
        }
    ]);
    (function () {
        var username = null;
        var roles = [];
        app.value('user', {
            signIn: function (loginInfo) {
                if (loginInfo != null) {
                    username = loginInfo.userName;
                    if (loginInfo.userRoles)
                        roles = loginInfo.userRoles;
                    else
                        roles = [];
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
            }
        });
    })();
    app.run(['user', 'abp.services.app.login', function (user, loginService) {
        loginService.getUserInfo().success(user.signIn);
    }]);
    app.run(['message', function (message) {
        abp.message.info = message.info;
        abp.message.warn = message.warning;
        abp.message.error = message.error;
    }]);
})();