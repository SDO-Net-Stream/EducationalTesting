using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Test;

namespace EduTesting.Service
{
    public interface ITestService
    {
        Test GetTest(int testId);
        IEnumerable<TestListItemViewModel> GetTests();

        Test InsertTest(Test test);

        void UpdateTest(Test test);

        void DeleteTest(TestListItemViewModel test);

        IEnumerable<Question> GetQuestions(int testId);

        IEnumerable<QuestionListItemViewModel> GetAllQuestions();

        Question InsertQuestion(Question test);

        void DeleteQuestion(QuestionListItemViewModel question);
    }
}
