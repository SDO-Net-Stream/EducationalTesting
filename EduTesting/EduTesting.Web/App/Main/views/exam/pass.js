(function () {
    var controllerId = 'app.views.exam.pass';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.exam', 'message', '$state', '$stateParams', 'testResult', 'enumConverter', '$q',
        function ($scope, examService, message, $state, $stateParams, testResult, enumConverter, $q) {
            var vm = this;
            $scope.examination = testResult;
            $scope.questionN = parseInt($stateParams.question);
            if (testResult.questions.length == 0) {
                message.error("Questions list is empty");
                $state.go('exam.list');
                return;
            }
            if ($scope.questionN <= 0 || $scope.questionN > testResult.questions.length || isNaN($scope.questionN)) {
                message.warning("Invalid question number");
                $state.go('exam.pass.question', { test: testResult.testId, question: 1 });
                return;
            }
            $scope.question = testResult.questions[$scope.questionN - 1];
            $scope.questionType = enumConverter.questionTypeToString($scope.question.questionType);

            $scope.goToQuestion = function (number) {
                $scope.waitSaving(true).then(function () {
                    $state.go('exam.pass.question', { test: testResult.testId, question: number });
                });
            };

            $scope.isQuestionAnswered = function (question) {
                question = question || $scope.question;
                switch (enumConverter.questionTypeToString(question.questionType)) {
                    case "SingleAnswer":
                    case "MultipleAnswers":
                        return question.userAnswer.answerIds.length > 0;
                        break;
                    case 'TextAnswer':
                        return !!question.userAnswer.answerText;
                    default:
                        throw "Not implemented";
                }
            };

            $scope.next = function () {
                $scope.waitSaving(true).then(function() {
                    // go to next unanswered question
                    for (var i = $scope.questionN % $scope.examination.questions.length; i != $scope.questionN - 1 ; i = (i + 1) % $scope.examination.questions.length) {
                        var question = $scope.examination.questions[i];
                        if (!$scope.isQuestionAnswered(question)) {
                            $scope.goToQuestion(i + 1);
                            return;
                        }
                    }
                    if ($scope.examination.questions[i].userAnswer.answerIds.length != 0) {
                        message.success("All answers saved. You can complete the test.");
                    }
                });
            };

            $scope.toggleAnswer = function (id) {
                var idx = $scope.question.userAnswer.answerIds.indexOf(id);
                if (idx > -1) {
                    $scope.question.userAnswer.answerIds.splice(idx, 1);
                } else {
                    switch($scope.questionType) {
                        case 'SingleAnswer':
                            $scope.question.userAnswer.answerIds = [id];
                            break;
                        case 'MultipleAnswers':
                            $scope.question.userAnswer.answerIds.push(id);
                            break;
                        default:
                            throw 'Not implemented';
                    }
                }
                $scope.trackChanges();
            };
            var countAnswers = function () {
                var count = 0;
                for (var i = 0; i < $scope.examination.questions.length; i++) {
                    var question = $scope.examination.questions[i];
                    if ($scope.isQuestionAnswered(question)) {
                        count++;
                    }
                }
                $scope.userAnswersCount = count;
                $scope.userAnswersFull = count == $scope.examination.questions.length;
            };
            countAnswers();
            var saveDefer = $q.defer();
            saveDefer.resolve();
            var saveTimeout = null;
            $scope.trackChanges = function (immediate) {
                countAnswers();
                if (saveTimeout) {
                    saveDefer.reject();
                    clearTimeout(saveTimeout);
                }
                saveDefer = $q.defer();
                var handler = function () {
                    saveTimeout = null;
                    var defer = saveDefer;
                    examService.saveUserAnswer($scope.question.userAnswer).success(function () {
                        message.success('Answer saved');
                        if (defer == saveDefer) {
                            defer.resolve();
                        }
                    });
                };
                if (immediate)
                    handler();
                else
                    saveTimeout = setTimeout(handler, 5000);
            };
            $scope.waitSaving = function (immediate) {
                if (saveTimeout && immediate) {
                    $scope.trackChanges(immediate);
                }
                return saveDefer.promise;
            };
            $scope.completeTest = function () {
                $scope.waitSaving(true).then(function () {
                    examService.completeTestResult({ testResultId: testResult.testResultId })
                        .success(function () {
                            message.success("Test completed");
                            $state.go('exam.list');
                        });
                });
            };
        }
    ]);
})();