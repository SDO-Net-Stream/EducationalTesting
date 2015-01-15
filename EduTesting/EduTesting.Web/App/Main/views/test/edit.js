(function () {
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

            vm.toggleAnswerRight = function (question, answer) {
                switch (question.questionTypeCode) {
                    case "SingleAnswer":
                        for (var i = 0; i < question.answers.length; i++)
                            question.answers[i].answerIsRight = false;
                        answer.answerIsRight = true;
                        break;
                    case "MultipleAnswers":
                        answer.answerIsRight = !answer.answerIsRight;
                        break;
                    default:
                        throw "Invalid question type";
                }
            };
            $scope.save = function () {
                // TODO: validate
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
