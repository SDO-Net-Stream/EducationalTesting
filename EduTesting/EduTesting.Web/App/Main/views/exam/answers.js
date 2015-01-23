(function () {
    var controllerId = 'app.views.exam.answers';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.exam', '$state', '$stateParams', 'enumConverter', 'user',
        function ($scope, examService, $state, $stateParam, enumConverter, user) {
            user.requireRole('User').then(function () {
                examService.getTestResult({ testResultId: $stateParam.result })
                    .success(function (result) {
                        $scope.model = result;
                        $scope.testResultStatus = enumConverter.testResultStatusToString(result.testResultStatus);
                    })
                    .error(function () {
                        $state.go('exam.list');
                    });
            });
        }
    ]);
})();