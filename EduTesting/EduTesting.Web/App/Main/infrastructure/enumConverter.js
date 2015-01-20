(function () {
    angular.module('app').service('enumConverter', function () {
        var questionTypes = ['SingleAnswer', 'MultipleAnswers', 'TextAnswer'];
        this.stringToQuestionType = function (str) {
            for (var i = 0; i < questionTypes.length; i++) {
                if (questionTypes[i] === str)
                    return i;
            }
            throw "Invalid QuestionType";
        };
        this.questionTypeToString = function (index) {
            if (index < 0 || index >= questionTypes.length)
                throw "Invalid QuestionType";
            return questionTypes[index];
        };

        var testResultStatuses = ['InProgress', 'Finished', 'Completed'];
        this.stringToTestResultStatus = function (str) {
            for (var i = 0; i < testResultStatuses.length; i++) {
                if (testResultStatuses[i] === str)
                    return i;
            }
            throw "Invalid TestResultStatus";
        };
        this.testResultStatusToString = function (index) {
            if (index < 0 || index >= testResultStatuses.length)
                throw "Invalid TestResultStatus";
            return testResultStatuses[index];
        };
    });
})();
