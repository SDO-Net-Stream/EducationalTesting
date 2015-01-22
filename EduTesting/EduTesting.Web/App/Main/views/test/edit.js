(function () {
    var controllerId = 'app.views.test.edit';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$stateParams', '$modal', 'enumConverter', 'user',
        function ($scope, testService, message, $state, $stateParams, $modal, enumConverter, user) {
            var vm = this;
            $scope.id = $stateParams.test;
            $scope.model = {
                testId: 0,
                questions: [],
                ratings:[]
            };
            if ($scope.id != 'new') {
                user.requireRole('Teacher').then(function () {
                    testService.getTest({ testId: $scope.id }).success(function (test) {
                        for (var i = 0; i < test.questions.length; i++)
                            test.questions[i].questionTypeCode = enumConverter.questionTypeToString(test.questions[i].questionType);
                        $scope.model = test;
                    });
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
                            testIsPublic: source.testIsPublic,
                            testTimeLimit: source.testTimeLimit,
                            testRandomSubsetSize: source.testRandomSubsetSize
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.testName = model.testName;
                            source.testDescription = model.testDescription;
                            source.testIsPublic = model.testIsPublic;
                            source.testTimeLimit = model.testTimeLimit;
                            source.testRandomSubsetSize = model.testRandomSubsetSize;
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
                            answerScore: 0,
                        };
                        var model = {
                            isNew: !answer,
                            answerText: source.answerText,
                            answerScore: source.answerScore
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.answerText = model.answerText;
                            source.answerScore = model.answerScore;
                            if (model.isNew) {
                                question.answers.push(source);
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
                    case "MultipleAnswers":
                        answer.answerScore = answer.answerScore ? 0 : 1;
                        break;
                    default:
                        throw "Invalid question type";
                }
            };

            $scope.editRating = function (rating) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.rating.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = rating || {
                            ratingId: 0,
                            ratingLowerBound: 0
                        };
                        var model = {
                            isNew: !rating,
                            ratingTitle: source.ratingTitle,
                            ratingScore: source.ratingLowerBound
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            source.ratingTitle = model.ratingTitle;
                            source.ratingLowerBound = model.ratingScore;
                            if (model.isNew) {
                                $scope.model.ratings.push(source);
                            }
                            $scopeModal.$close(source);
                            //TODO: sort ratings by score
                        };
                        $scopeModal.cancel = function () {
                            $scopeModal.$dismiss('cancel');
                        };
                    }]
                });
            };
            $scope.deleteRating = function (rating) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.deleteRating.html',
                    controller: ['$scope', function ($scopeModal) {
                        $scopeModal.model = rating;
                        $scopeModal.ok = function () {
                            var ratings = $scope.model.ratings;
                            for (var i = 0; i < ratings.length; i++) {
                                if (ratings[i] == rating) {
                                    ratings.splice(i, 1);
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
                                if (answer.answerScore > 0)
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
