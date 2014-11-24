(function () {
    var controllerId = 'app.views.question.list';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$modal',
        function ($scope, testService, message, $state, $modal) {
            $scope.questions = [];
            var loadQuestions = function () {
                testService.getAllQuestions().success(function (list) {
                    $scope.questions = list;
                });
            };
            loadQuestions();
            $scope.create = function () {
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
            $scope.delete = function (question) {
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
