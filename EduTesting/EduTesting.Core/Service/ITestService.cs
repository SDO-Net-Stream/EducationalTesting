using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Question;
using EduTesting.ViewModels.Test;

namespace EduTesting.Service
{
    public interface ITestService
    {
        TestListItemViewModel GetTest(int testId);
        IEnumerable<TestListItemViewModel> GetTests();

        TestListItemViewModel InsertTest(TestListItemViewModel test);
        void UpdateTest(TestListItemViewModel test);
        void DeleteTest(TestListItemViewModel test);

        IEnumerable<QuestionListItemViewModel> GetQuestions(int testId);
        IEnumerable<QuestionListItemViewModel> GetAllQuestions();
        QuestionListItemViewModel InsertQuestion(QuestionListItemViewModel test);
        void DeleteQuestion(QuestionListItemViewModel question);
    }
}
