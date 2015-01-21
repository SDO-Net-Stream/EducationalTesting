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
            var provideAuth = function (roleName) {
                return [
                    '$state', 'user', 'message',
                    function ($state, user, message) {
                        user.requireRole(roleName).then(angular.noop, function () {
                            message.error('Not enought permissions');
                            $state.go('home');
                        })
                    }
                ];
            };

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
                    template: '<ui-view/>',
                    onEnter: provideAuth('Teacher')
                })
                .state('test.list', {
                    url: '/list',
                    templateUrl: '/App/Main/views/test/list.cshtml',
                    menu: 'Test'
                })
                .state('test.edit', {
                    url: '/:test/edit',
                    templateUrl: '/App/Main/views/test/edit.cshtml',
                    menu: 'Test'
                })
                .state('test.result', { // test results
                    url: '/:test/result',
                    menu: 'Test'
                })
                .state('test.result.details', {
                    url: '/:user',
                    menu: 'Test'
                })


                .state('exam', {
                    abstract: true,
                    url: '/exam',
                    template: '<ui-view/>',
                    onEnter: provideAuth('User')
                })
                .state('exam.list', {
                    url: '/list',
                    templateUrl: '/App/Main/views/exam/list.cshtml',
                    menu: 'Exam'
                })
                .state('exam.pass', { // answering test
                    url: '/:test/pass',
                    abstract: true,
                    resolve: {
                        testResult: [
                            'abp.services.app.exam', '$stateParams', '$q', '$state', 'user',
                            function (resultService, $stateParams, $q, $state, user) {
                                var defer = $q.defer();
                                user.requireRole('User').then(function () {
                                    var result = resultService.getActiveUserTestResult({ testId: $stateParams.test });
                                    result.success(function (testResult) { defer.resolve(testResult); });
                                    result.error(function () {
                                        $state.go('exam.list');
                                        defer.reject();
                                    });
                                }, function () {
                                    defer.reject();
                                });
                                return defer.promise;
                            }
                        ]
                    },
                    template: '<ui-view/>'
                })
                .state('exam.pass.question', {
                    url: '/:question',
                    templateUrl: '/App/Main/views/exam/pass.cshtml',
                    controller: 'app.views.exam.pass',
                    menu: 'Exam'
                })


                .state('group', {
                    abstract: true,
                    url: '/group',
                    template: '<ui-view/>',
                    onEnter: provideAuth('Teacher')
                })
                .state('group.list', {
                    url: '/list/:group',
                    params: {
                        group: { value: 0 }
                    },
                    templateUrl: '/App/Main/views/group/list.cshtml',
                    menu: 'Group'
                })
                .state('user', {
                    abstract: true,
                    url: '/user',
                    template: '<ui-view/>',
                    onEnter: provideAuth('Administrator')
                })
                .state('user.list', {
                    url: '/list',
                    templateUrl: '/App/Main/views/user/list.cshtml',
                    menu: 'User'
                })
            ;
        }
    ]);
    app.run(['message', function (message) {
        abp.message.info = message.info;
        abp.message.warn = message.warning;
        abp.message.error = message.error;
    }]);
})();