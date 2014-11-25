(function () {
    var controllerId = 'app.views.test.pass';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.testResult', 'message', '$state', '$stateParams', 'testResult', 'enumConverter',
        function ($scope, examService, message, $state, $stateParams, testResult, enumConverter) {
            var vm = this;
            $scope.examination = testResult;
            $scope.questionN = parseInt($stateParams.question);
            if (testResult.questions.length == 0) {
                message.error("Questions list is empty");
                $state.go('test.list');
                return;
            }
            if ($scope.questionN <= 0 || $scope.questionN > testResult.questions.length || isNaN($scope.questionN)) {
                message.warning("Invalid question number");
                $state.go('test.pass.question', { test: testResult.testId, question: 1 });
                return;
            }
            $scope.question = testResult.questions[$scope.questionN - 1];
            $scope.questionType = enumConverter.questionTypeToString($scope.question.questionType);
            if ($scope.question.questionType == 0) { // single answer
                if ($scope.question.userAnswer.answersId.length > 0) {
                    $scope.answerId = $scope.question.userAnswer.answersId[0];
                } else {
                    $scope.answerId = -1;
                }
                $scope.$watch('answerId', function (newValue) {
                    if ($scope.answerId >= 0) {
                        $scope.question.userAnswer.answersId = [newValue];
                        message.info('Answer detected: ' + newValue);
                    } else {
                        $scope.question.userAnswer.answersId = [];
                    }
                });
            }
            $scope.goToQuestion = function (number) {
                $state.go('test.pass.question', { test: testResult.testId, question: number });
            };
            $scope.next = function () {
                examService.saveUserAnswer($scope.question.userAnswer).success(function () {
                    message.success('Answer saved');
                    // go to next unanswered question
                    for (var i = $scope.questionN; i != $scope.questionN - 1 ; i++) {
                        i = i % $scope.examination.questions.length;
                        var question = $scope.examination.questions[i];
                        switch (enumConverter.questionTypeToString(question.questionType)) {
                            case "SingleAnswer":
                                if (question.userAnswer.answersId.length == 0) {
                                    $scope.goToQuestion(i + 1);
                                    return;
                                }
                                break;
                            default:
                                throw "Not implemented";
                        }
                    }
                    message.success("All answers saved. You can complete the test.");
                });
            };
        }
    ]);
})();