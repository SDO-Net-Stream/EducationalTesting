﻿(function () {
    var controllerId = 'app.views.test.edit';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$stateParams', '$modal', 'enumConverter',
        function ($scope, testService, message, $state, $stateParams, $modal, enumConverter) {
            var vm = this;
            $scope.id = $stateParams.test;
            $scope.model = {
                testId: 0,
                questions: []
            };
            if ($scope.id != 'new') {
                testService.getTest({ testId: $scope.id }).success(function (test) {
                    for (var i = 0; i < test.questions.length; i++)
                        test.questions[i].questionTypeCode = enumConverter.questionTypeToString(test.questions[i].questionType);
                    $scope.model = test;
                });
            }

            $scope.editTestName = function () {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.name.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = $scope.model;
                        var model = {
                            testName: source.testName,
                            testDescription: source.testDescription,
                            testType: source.testType
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.testName = model.testName;
                            source.testDescription = model.testDescription;
                            source.testType = model.testType;
                            $scopeModal.$close(source);
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };

            $scope.editQuestion = function (question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.question.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = question || {
                            questionId: 0,
                            questionTypeCode: 'SingleAnswer',
                            answers: []
                        };
                        var model = {
                            isNew: !question,
                            questionText: source.questionText,
                            questionType: source.questionTypeCode,
                            questionDescription: source.questionDescription
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.questionText = model.questionText;
                            source.questionDescription = model.questionDescription;
                            source.questionTypeCode = model.questionType;
                            source.questionType = enumConverter.stringToQuestionType(model.questionType);
                            if (model.isNew) {
                                $scope.model.questions.push(source);
                            }
                            $scopeModal.$close(source);
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };
            $scope.deleteQuestion = function (question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.deleteQuestion.html',
                    controller: ['$scope', function ($scopeModal) {
                        $scopeModal.model = question;
                        $scopeModal.ok = function () {
                            var questions = $scope.model.questions;
                            for (var i = 0; i < questions.length; i++) {
                                if (questions[i] == question) {
                                    questions.splice(i, 1);
                                    break;
                                }
                            }
                            $scopeModal.$close();
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };

            $scope.editAnswer = function (answer, question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.answer.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = answer || {
                            answerId: 0,
                            answerIsRight: false,
                        };
                        var model = {
                            isNew: !answer,
                            answerText: source.answerText,
                            answerIsRight: source.answerIsRight
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.answerText = model.answerText;
                            if (model.isNew) {
                                question.answers.push(source);
                            }
                            if (source.answerIsRight != model.answerIsRight) {
                                vm.toggleAnswerRight(question, source);
                            }

                            $scopeModal.$close(source);
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };
            $scope.deleteAnswer = function (answer, question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.deleteAnswer.html',
                    controller: ['$scope', function ($scopeModal) {
                        $scopeModal.model = answer;
                        $scopeModal.ok = function () {
                            var answers = question.answers;
                            for (var i = 0; i < answers.length; i++) {
                                if (answers[i] == answer) {
                                    answers.splice(i, 1);
                                    break;
                                }
                            }
                            $scopeModal.$close();
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };

            vm.toggleAnswerRight = function (question, answer) {
                switch (question.questionTypeCode) {
                    case "SingleAnswer":
                        /*
                        for (var i = 0; i < question.answers.length; i++)
                            question.answers[i].answerIsRight = false;
                        answer.answerIsRight = true;
                        break;
                        */
                    case "MultipleAnswers":
                        answer.answerIsRight = !answer.answerIsRight;
                        break;
                    default:
                        throw "Invalid question type";
                }
            };
            $scope.highlightQuestion = function (question) {
                var questions = $scope.model.questions;
                for (var i = 0; i < questions.length; i++)
                    if (questions[i] != question)
                        questions[i].$isopen = false;
                if (question) {
                    question.$isopen = true;
                    question.$error = true;
                }
            };
            $scope.validate = function () {
                var test = $scope.model;
                if (!test.testName) {
                    message.error("Please enter name of the test");
                    return false;
                }
                for (var i = 0; i < test.questions.length; i++) {
                    test.questions[i].$error = false;
                }
                for (var i = 0; i < test.questions.length; i++) {
                    var question = test.questions[i];
                    var questionTitle = question.questionText || question.questionDescription;
                    if (!questionTitle) {
                        message.error("Please enter question text");
                        $scope.highlightQuestion(question);
                        return false;
                    }
                    switch (question.questionTypeCode) {
                        case 'TextAnswer':
                            if (question.answers.length != 0) {
                                message.error(questionTitle, "Question with type 'Custom answer' should not contain predefined answers");
                                $scope.highlightQuestion(question);
                                return false;
                            }
                            break;
                        case 'SingleAnswer':
                        case 'MultipleAnswers':
                            if (question.answers.length == 0) {
                                message.error(questionTitle, "Question should contain answers");
                                $scope.highlightQuestion(question);
                                return false;
                            }
                            var right = 0;
                            for (var j = 0; j < question.answers.length; j++) {
                                var answer = question.answers[j];
                                if (!answer.answerText) {
                                    message.error(questionTitle, "Answer text should be not empty");
                                    $scope.highlightQuestion(question);
                                    return false;
                                }
                                if (answer.answerIsRight)
                                    right++;
                            }
                            if (right == 0) {
                                message.error(questionTitle, "No answers marked as right");
                                $scope.highlightQuestion(question);
                                return false;
                            }
                            break;
                    }
                }
                return true;
            };
            $scope.save = function () {
                if (!$scope.validate())
                    return;
                var result;
                if ($scope.id == 'new') {
                    result = testService.insertTest($scope.model);
                } else {
                    result = testService.updateTest($scope.model);
                }
                result.success(function (response) {
                    if ($scope.id == 'new')
                        message.success("Test successfully created");
                    else
                        message.success("Test successfully updated");
                    $state.go('test.list');
                });
            };
        }
    ]);

})();