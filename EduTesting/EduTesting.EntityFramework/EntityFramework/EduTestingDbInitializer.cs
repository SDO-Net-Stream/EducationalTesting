using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EduTesting.Model;

namespace EduTesting.EntityFramework
{
    public class EduTestingDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EduTestingDbContext>
    {
        protected override void Seed(EduTestingDbContext context)
        {
            var tests = new List<Test>
            {
                new Test{TestId = 1, TestName = "Pre-Intermediate", NumberOfQuestions = 7},
                new Test{TestId = 2, TestName = "Intermediate", NumberOfQuestions = 8}
            };
            tests.ForEach(t => context.Tests.Add(t));
            context.SaveChanges();

            var questions = new List<Question>
            {
                new Question
                {
                    QuestionId = 1,
                    TestId = 1,
                    QuestionText = "Because the first pair of shoes did not fit properly, he asked for ... .",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 2,
                    TestId = 1,
                    QuestionText = "... the Boston Red Sox have often been outstanding, they haven’t won the World Series since 1918.",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 3,
                    TestId = 1,
                    QuestionText = ". ... many computer software programs that possess excellent word-processing capabilities",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 4,
                    TestId = 1,
                    QuestionText = "Many Middle Eastern diplomats still feel that the USA is intent ... the ultimate policeman in the region.",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 5,
                    TestId = 1,
                    QuestionText = "Woodrow Wilson believed the United States' entry into World War I would ... the war in months",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 6,
                    TestId = 1,
                    QuestionText = "... of New York's Erie Canal greatly enhanced trade in the upstate region",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 7,
                    TestId = 1,
                    QuestionText = "After ... the skin, a leech is best removed by the application of either salt or heat.",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 8,
                    TestId = 2,
                    QuestionText = "... east of the Mississippi River.",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 9,
                    TestId = 2,
                    QuestionText = "... wrote the operetta \"Babes in Toyland\", drawn from the childhood characters of Mother Goose",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 10,
                    TestId = 2,
                    QuestionText = "Some of the oldest and most widespread creation myths are ... involving the \"Earth Mother\"",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 11,
                    TestId = 2,
                    QuestionText = "In ... , compact disk technology has almost made record albums obsolete.",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 12,
                    TestId = 2,
                    QuestionText = ". In the first few months of life, an infant learns how to lift its hands, how to smile and ... ",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 13,
                    TestId = 2,
                    QuestionText = "Juana Inez de la Cruz ... Mexico's greatest female poet",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 14,
                    TestId = 2,
                    QuestionText = "Because the metal mercury ... in direct proportion to temperature, it was once used as the indicator in common thermometers",
                    QuestionDescription = "Choose write answer from a to d"
                },
                new Question
                {
                    QuestionId = 15,
                    TestId = 2,
                    QuestionText = "... what is now San Salvador, Christopher Columbus believed that he had found Japan.",
                    QuestionDescription = "Choose write answer from a to d"
                }
            };
            questions.ForEach(q => context.Questions.Add(q));
            context.SaveChanges();

            context.Attributes.Add(new CustomAttribute { AttributeID = 1, AttributeName = "Question Type" });
            context.SaveChanges();

            var singleAnswerTypeValue = EduTesting.ViewModels.TestResult.QuestionType.SingleAnswer.GetTypeCode().ToString(); 
            var questionAttributes = new List<QuestionAttribute>
            {
                new QuestionAttribute{QuestionID = 1, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 2, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 3, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 4, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 5, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 6, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 7, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 8, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 9, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 10, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 11, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 12, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 13, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 14, AttributeID = 1, Value = singleAnswerTypeValue},
                new QuestionAttribute{QuestionID = 15, AttributeID = 1, Value = singleAnswerTypeValue}
            };
            questionAttributes.ForEach(q => context.QuestionAttributes.Add(q));
            context.SaveChanges();

            var answers = new List<Answer>
            {
                new Answer{QuestionId = 1, AnswerText = "another pants"},
                new Answer{QuestionId = 1, AnswerText = "other pants"},
                new Answer{QuestionId = 1, AnswerText = "the other ones"}, 
                new Answer{QuestionId = 1, AnswerText = "another pair"},
                new Answer{QuestionId = 2, AnswerText = "However"},
                new Answer{QuestionId = 2, AnswerText = "Yet"},
                new Answer{QuestionId = 2, AnswerText = "That"},
                new Answer{QuestionId = 2, AnswerText = "Although"},
                new Answer{QuestionId = 3, AnswerText = "There are"},
                new Answer{QuestionId = 3, AnswerText = "The"},
                new Answer{QuestionId = 3, AnswerText = "There is a lot of"}, 
                new Answer{QuestionId = 3, AnswerText = "Some"},
                new Answer{QuestionId = 4, AnswerText = "to being"},
                new Answer{QuestionId = 4, AnswerText = "being"},
                new Answer{QuestionId = 4, AnswerText = "be"}, 
                new Answer{QuestionId = 4, AnswerText = "on being"},
                new Answer{QuestionId = 5, AnswerText = "to finish"},
                new Answer{QuestionId = 5, AnswerText = "finish"},
                new Answer{QuestionId = 5, AnswerText = "finishing"},
                new Answer{QuestionId = 5, AnswerText = "will have finished"},
                new Answer{QuestionId = 6, AnswerText = "The complete"},
                new Answer{QuestionId = 6, AnswerText = "Completing"},
                new Answer{QuestionId = 6, AnswerText = "A completing"},
                new Answer{QuestionId = 6, AnswerText = "The completion"},
                new Answer{QuestionId = 7, AnswerText = "it attaches to"},
                new Answer{QuestionId = 7, AnswerText = "attaching to"},
                new Answer{QuestionId = 7, AnswerText = "its attaching to"},
                new Answer{QuestionId = 7, AnswerText = "where it attaches to"},
                new Answer{QuestionId = 8, AnswerText = "Indigo was grown usually"},
                new Answer{QuestionId = 8, AnswerText = "Usually grown was Indigo"},
                new Answer{QuestionId = 8, AnswerText = "Indigo usually grown"},
                new Answer{QuestionId = 8, AnswerText = "Indigo was usually grown"},
                new Answer{QuestionId = 9, AnswerText = "That was Victor Herbert who"},
                new Answer{QuestionId = 9, AnswerText = "Victor Herbert who"},
                new Answer{QuestionId = 9, AnswerText = "Since it was Victor Herbert"}, 
                new Answer{QuestionId = 9, AnswerText = "It was Victor Herbert who"},
                new Answer{QuestionId = 10, AnswerText = "those"},
                new Answer{QuestionId = 10, AnswerText = "them"},
                new Answer{QuestionId = 10, AnswerText = "they"},
                new Answer{QuestionId = 10, AnswerText = "their"},
                new Answer{QuestionId = 11, AnswerText = "the decade from"},
                new Answer{QuestionId = 11, AnswerText = "the decade since"},
                new Answer{QuestionId = 11, AnswerText = "the past decade"},
                new Answer{QuestionId = 11, AnswerText = "decade ago the"},
                new Answer{QuestionId = 12, AnswerText = "how its parents to recognize"},
                new Answer{QuestionId = 12, AnswerText = "how to recognize its parents"},
                new Answer{QuestionId = 12, AnswerText = "to be recognizing its parents"},
                new Answer{QuestionId = 12, AnswerText = "the recognizing of its parents"},
                new Answer{QuestionId = 13, AnswerText = "considered"},
                new Answer{QuestionId = 13, AnswerText = "considered to be"},
                new Answer{QuestionId = 13, AnswerText = "is considered to be"},
                new Answer{QuestionId = 13, AnswerText = "is consideration"},
                new Answer{QuestionId = 14, AnswerText = "is expanding"},
                new Answer{QuestionId = 14, AnswerText = "expands"},
                new Answer{QuestionId = 14, AnswerText = "is expanded"},
                new Answer{QuestionId = 14, AnswerText = "expanded"},
                new Answer{QuestionId = 15, AnswerText = "He reached"},
                new Answer{QuestionId = 15, AnswerText = "When did he reach"},
                new Answer{QuestionId = 15, AnswerText = "Having reached"},
                new Answer{QuestionId = 15, AnswerText = "Whether he reached"}
            };
            answers.ForEach(a => context.Answers.Add(a));
            context.SaveChanges();
        }
    }
}
