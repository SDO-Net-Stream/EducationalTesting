﻿(function () {
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
        '$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {
            //$locationProvider.html5Mode(false);
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
                    url: '/test',
                    templateUrl: '/App/Main/views/test/list.cshtml',
                })
                .state('test.edit', {
                    url: '^/test/edit/:test',
                    templateUrl: '/App/Main/views/test/list.cshtml',
                })
                .state('question', {
                    url: '/question',
                    templateUrl: '/App/Main/views/question/list.cshtml',
                })
                .state('question.edit', {
                    url: '^/question/edit/:test/:question',
                    templateUrl: '/App/Main/views/question/list.cshtml',
                })
            ;
        }
    ]);
    (function () {
        var username = null;
        app.value('user', {
            signIn: function (loginInfo) {
                if (loginInfo != null) {
                    username = loginInfo.userName;
                }
            },
            isAuthenticated: function () {
                return !!username;
            },
            name: function () {
                return username;
            }
        });
    })();
    app.run(['user', 'abp.services.app.login', function(user, loginService) {
        loginService.getUserInfo().success(user.signIn);
    }]);
    app.run(['message', function (message) {
        abp.message.info = message.info;
        abp.message.warn = message.warning;
        abp.message.error = message.error;
    }]);
})();