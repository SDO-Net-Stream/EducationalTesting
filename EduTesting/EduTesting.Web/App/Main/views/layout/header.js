﻿(function () {
    var controllerId = 'app.views.layout.header';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'user', 'abp.services.app.login', '$location', 'message',
        function ($rootScope, $state, user, loginService, $location, message) {
            var vm = this;

            vm.languages = abp.localization.languages;
            vm.currentLanguage = abp.localization.currentLanguage;

            vm.menu = abp.nav.menus.MainMenu;
            vm.currentMenuName = $state.current.menu;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                vm.currentMenuName = toState.menu;
            });
            vm.user = user;
            vm.logOff = function () {
                loginService.logOff().success(function () {
                    user.signIn(false);
                    message.info('Logout successfully completed.');
                    $location.path('/');
                });
                return false;
            };
        }
    ]);
})();