using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using EducationalProject.DataInfo;

namespace EducationalProject.Models
{
    public class TestWrapper
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public DateTime? DateDownload { get; set; }
        public String Order { get; set; }
    }

    public class TestInfoWrapper
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int CountQuestions { get; set; }
    }

    public class QuestionInProgresWrapper
    {
        public int Number { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public string TestType{ get; set; }
        public string QuestionText{ get; set; }
        public int QuestionCount{ get; set; }
        public string SelectedAnswer { set; get; }
        public List<VariantItem> AnswerVariantList { get; set; }
        public List<StatusQuestion> StatusList { get; set; }
        public QuestionInProgresWrapper(){}
        public QuestionInProgresWrapper(int number, TestInProgres currentQeustion, Action action)
        {
            Number = number;
            TestType = currentQeustion.Question.TestType;
            QuestionText = currentQeustion.Question.Text;
            HasPrev = action.TestsInProgres.FirstOrDefault(q => q.Question.Number == (number - 1)) != null;
            HasNext = action.TestsInProgres.FirstOrDefault(q => q.Question.Number == (number + 1)) != null;
            AnswerVariantList = new List<VariantItem>();
            QuestionCount = ((QuestionWithVariants) currentQeustion.Question).VariantAnswers.Count;
            SelectedAnswer = ConvertAnswer(currentQeustion.Question.TestType, currentQeustion.UserAnswer);
            StatusList = new List<StatusQuestion>();

            foreach (var variant in ((QuestionWithVariants) currentQeustion.Question).VariantAnswers)
            {
                var variantId =
                    ((QuestionWithVariants) currentQeustion.Question).VariantAnswers.IndexOf(variant);
                AnswerVariantList.Add(new VariantItem
                {
                    Text = variant.Text,
                    VariantId = variantId,
                    Selected =
                        InitializeChecked(currentQeustion.Question.TestType, currentQeustion.UserAnswer,
                            variantId)
                });
            }

            foreach (var index in action.TestsInProgres.Select(test => action.TestsInProgres.IndexOf(test) + 1))
            {
                StatusList.Add(new StatusQuestion
                {
                    Number = index,
                    IsActive = index == number
                });
            }
        }

        private string ConvertAnswer(String type, string answer)
        {
            if (answer == null)
            {
                return null;
            }
            string convertedAnswer = null;
            switch (type)
            {
                case DataConst.CheckedType:
                {
                    convertedAnswer = answer;
                    break;
                }
                case DataConst.RadioType:
                {
                    foreach (var letter in answer)
                    {
                        if (letter != '1') continue;
                        convertedAnswer = Convert.ToString(answer.IndexOf(letter));
                        break;
                    }
                }
                    break;
            }
            return convertedAnswer;
        }

        private static bool InitializeChecked(string testType, string answer, int variantId)
        {
            if (testType == DataConst.CheckedType && answer != null)
            {
                if (answer.Count() >= variantId)
                {
                    return answer[variantId] == '1';
                }
            }
            return false;
        }
    }

    public class VariantItem
    {
        public int VariantId { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
    }

    public class StatusQuestion
    {
        public int Number { get; set; }
        public bool IsActive { get; set; }
    }

    public class ResultsWrapper
    {
        public int TestResultId { get; set; }
        public string TestName { get; set; }
        public DateTime? DatePassing { get; set; }
        public int? PercentTaken { get; set; }
        public bool? Passed { get; set; }
    }

    public class DetailsResultWrapper
    {
        public string TestName { get; set; }
        public int? PercentTaken { get; set; }
        public bool? Passed { get; set; }
        public List<QuestionDetails> QuestionsList { get; set; }
    }

    public class QuestionDetails
    {
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public string UserNumber { get; set; }
        public string RealNumber { get; set; }
    }

    public class CheckedQuestion : QuestionDetails
    {
        public List<CheckedVariant> VariantsList { get; set; }
    }

    public class CheckedVariant
    {
        public bool UserAnswer { get; set; }
        public bool RealAnswer { get; set; }
        public string Text { get; set; }
    }

    public class ResponseAction
    {
        public string Command { get; set; }
        public int? CurrentNumber { get; set; }
        public int? GoTo { get; set; }

        public ResponseAction(string response)
        {
            string[] param = response.Split(';');
            if (param.Count() > 1)
            {
                Command = param[0];
                int current;
                if (int.TryParse(param[1], out current))
                {
                    CurrentNumber = current;
                }
            }
            if (param.Count() == 3)
            {
                int goTo;
                if (int.TryParse(param[2], out goTo))
                {
                    GoTo = goTo;
                }
            }
        }
    }
}