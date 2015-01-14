(function () {
    var controllerId = 'app.views.test.edit';
    var app = angular.module('app');
    app.controller(controllerId, [
        '$scope', 'abp.services.app.test', 'message', '$state', '$stateParams', '$modal', 'enumConverter',
        function ($scope, testService, message, $state, $stateParams, $modal, enumConverter) {
            var vm = this;
            $scope.id = $stateParams.test;
            $scope.model = {};
            if ($scope.id != 'new') {
                testService.getTest({ testId: $scope.id }).success(function (test) {
                    for (var i = 0; i < test.questions.length; i++)
                        test.questions[i].questionTypeCode = enumConverter.questionTypeToString(test.questions[i].questionType);
                    $scope.model = test;
                });
            }

            $scope.editQuestion = function (question) {
                var dialog = $modal.open({
                    templateUrl: 'app.views.test.edit.question.html',
                    controller: ['$scope', function ($scopeModal) {
                        var source = question || { questionId: 0, questionTypeCode: 'SingleAnswer' };
                        var model = {
                            isNew: !question,
                            questionText: source.questionText,
                            questionType: source.questionTypeCode,
                            questionDescription: source.questionDescription
                        };
                        $scopeModal.model = model;
                        $scopeModal.ok = function () {
                            var result = {
                                questionId: source.questionId,
                                questionText: model.questionText,
                                questionTypeCode: model.questionType,
                                questionType: enumConverter.stringToQuestionType(model.questionType),
                                questionDescription: model.questionDescription
                            };
                            var questions = $scope.model.questions;
                            if (!$scopeModal.model.isNew) {
                                for (var i = 0; i < questions.length; i++) {
                                    if (questions[i] == question) {
                                        questions[i] = result;
                                        break;
                                    }
                                }
                            } else {
                                questions.push(result);
                            }
                            $scopeModal.$close(result);
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
        }
    ]);
    
})();
