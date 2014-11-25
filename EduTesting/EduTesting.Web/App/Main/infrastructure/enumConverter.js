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
    });
})();
