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

        var userRoles = ['User', 'Teacher', 'Administrator'];
        this.stringToUserRole = function (str) {
            for (var i = 0; i < userRoles.length; i++) {
                if (userRoles[i] === str)
                    return i + 1;
            }
            throw "Invalid UserRole";
        };
        this.userRoleToString = function (index) {
            if (index < 1 || index > testResultStatuses.length)
                throw "Invalid UserRole";
            return userRoles[index - 1];
        };

        var testStatuses = ['Pending', 'Published'];
        this.stringToTestStatus = function (str) {
            for (var i = 0; i < testStatuses.length; i++) {
                if (testStatuses[i] === str)
                    return i;
            }
            throw "Invalid TestStatus";
        };
        this.testStatusToString = function (index) {
            if (index < 0 || index >= testStatuses.length)
                throw "Invalid TestStatus";
            return testStatuses[index];
        };

    });
})();
