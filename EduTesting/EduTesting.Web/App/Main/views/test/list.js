(function () {
    var controllerId = 'app.views.test.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal',
        function ($scope, testService, message, $state, $modal) {
            $scope.tests = [];
            var loadTests = function () {
                testService.getTests().success(function (list) {
                    $scope.tests = list;
                });
            };
            loadTests();
            $scope.createTest = function () {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.list.add.html',
                    controller: function ($scope) {
                        $scope.model = { testName: "" };
                        $scope.ok = function () {
                            testService
                                .insertTest($scope.model)
                                .success(function (test) {
                                    message.success("Test '" + test.testName + "' successfully created");
                                    $scope.$close(test);
                                    $state.go('test.edit', { test: test.testId });
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
            $scope.createQuestion = function () {
                var dialog = $modal.open({
                    templateUrl: 'app.views.question.list.add.html',
                    controller: function ($scope) {
                        $scope.model = { questionText: "" };
                        $scope.ok = function () {
                            testService
                                .insertQuestion($scope.model)
                                .success(function (question) {
                                    message.success("Question '" + question.questionText + "' successfully created");
                                    $scope.$close(question);
                                    $state.go('question.edit', { question: question.questionId });
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
            $scope.deleteTest = function (test) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.list.delete.html',
                    controller: function ($scope) {
                        $scope.model = test;
                        $scope.ok = function () {
                            testService.deleteTest(test)
                                .success(function () {
                                    message.success("Test '" + test.testName + "' successfully deleted");
                                    loadTests();
                                    $scope.$close(test);
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
			$scope.start = function (test) {
                testResultService.startTest({ testId: test.testId }).success(function () {
                    message.success("Test '" + test.testName + "' successfully started");
                    $state.go('test.pass.question', { test: test.testId, question: 1 });
                });
            };
            $scope.deleteQuestion = function (question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.question.list.delete.html',
                    controller: function ($scope) {
                        $scope.model = question;
                        $scope.ok = function () {
                            testService.deleteQuestion(question)
                                .success(function () {
                                    message.success("Question '" + question.questionText + "' successfully deleted");
                                    loadQuestions();
                                    $scope.$close(question);
                                });
                        };
                        $scope.cancel = function () {
                            $scope.$dismiss('cancel');
                        };
                    }
                });
            };
        }
    ]);
})();
