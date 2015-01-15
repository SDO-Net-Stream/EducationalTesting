(function () {
    var controllerId = 'app.views.test.pass';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.testResult', 'message', '$state', '$stateParams', 'testResult', 'enumConverter', '$q',
        function ($scope, examService, message, $state, $stateParams, testResult, enumConverter, $q) {
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
            if ($scope.questionType == 'SingleAnswer') {
                if ($scope.question.userAnswer.answerIds.length > 0) {
                    $scope.answerId = $scope.question.userAnswer.answerIds[0];
                } else {
                    $scope.answerId = -1;
                }
                $scope.$watch('answerId', function (newValue) {
                    console.log(newValue);
                    if ($scope.answerId >= 0) {
                        if ($scope.question.userAnswer.answerIds.length == 0 || $scope.question.userAnswer.answerIds[0] != newValue) {
                            $scope.question.userAnswer.answerIds = [newValue];
                            message.info('Answer detected: ' + newValue);
                            $scope.trackChanges();
                        } else {
                            message.info('Same detected: ' + newValue);
                        }
                    } else {
                        $scope.question.userAnswer.answerIds = [];
                    }
                });
            }
            if ($scope.questionType == 'MultipleAnswers') {
                $scope.answerIds = $scope.question.userAnswer.answerIds;
            }
            $scope.goToQuestion = function (number) {
                $state.go('test.pass.question', { test: testResult.testId, question: number });
            };
            $scope.next = function () {
                $scope.waitSaving(true).then(function() {
                    // go to next unanswered question
                    for (var i = $scope.questionN % $scope.examination.questions.length; i != $scope.questionN - 1 ; i = (i + 1) % $scope.examination.questions.length) {
                        var question = $scope.examination.questions[i];
                        switch (enumConverter.questionTypeToString(question.questionType)) {
                            case "SingleAnswer":
                            case "MultipleAnswers":
                                if (question.userAnswer.answerIds.length == 0) {
                                    $scope.goToQuestion(i + 1);
                                    return;
                                }
                                break;
                            default:
                                throw "Not implemented";
                        }
                    }
                    if ($scope.examination.questions[i].userAnswer.answerIds.length != 0) {
                        message.success("All answers saved. You can complete the test.");
                    }
                });
            };
            $scope.checkAnswer = function (id) {
                var idx = $scope.question.userAnswer.answerIds.indexOf(id);
                if (idx > -1) {
                    $scope.question.userAnswer.answerIds.splice(idx, 1);
                } else {
                    $scope.question.userAnswer.answerIds.push(id);
                }
                $scope.answerIds = $scope.question.userAnswer.answerIds;
                console.log($scope.question.userAnswer.answerIds);
                $scope.trackChanges();
            };
            console.log($scope.question.userAnswer.answerIds);
            var saveDefer = $q.defer();
            saveDefer.resolve();
            var saveTimeout = null;
            $scope.trackChanges = function (immediate) {
                console.log('trackChanges', immediate);
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
        }
    ]);
})();