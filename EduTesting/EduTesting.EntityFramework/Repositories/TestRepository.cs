using System.Collections.Generic;
using System.Linq;
using EduTesting.Model;
using EduTesting.Interfaces;

namespace EduTesting.Repositories
{
    public class TestRepository : ITestRepository
    {
        private List<int> _rightAnswersEnglishTest0 = new List<int>
        {
            3, 3, 0, 3, 1, 3, 0
        };
        private List<int> _rightAnswersEnglishTest1 = new List<int>
        {
            3, 3, 0, 2, 1, 2, 1, 2, 0, 1, 3, 1, 0, 2
        };
        private static List<Answer> _answers0_0 = new List<Answer>
        {
            new Answer(0,"another pants"),new Answer(1,"other pants"),new Answer(2,"the other ones"), new Answer(3,"another pair", true)
        };
        private static Question _question00 = new Question
        {
            QuestionId = 1,
            TestId = 1,
            QuestionText = "Because the first pair of shoes did not fit properly, he asked for ... .",
            Answers = _answers0_0,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };

        private static List<Answer> _answers0_1 = new List<Answer>
        {
            new Answer(0,"However"),new Answer(1,"Yet"),new Answer(2,"That"), new Answer(3,"Although", true)
        };
        private static Question _question01 = new Question
        {
            QuestionId = 2,
            TestId = 1,
            QuestionText = "... the Boston Red Sox have often been outstanding, they haven’t won the World Series since 1918.",
            Answers = _answers0_1,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };

        private static List<Answer> _answers0_2 = new List<Answer>
        {
            new Answer(0,"There are", true),new Answer(1,"The"),new Answer(2,"There is a lot of"), new Answer(3,"Some")
        };
        private static Question _question02 = new Question
        {
            QuestionId = 3,
            TestId = 1,
            QuestionText = ". ... many computer software programs that possess excellent word-processing capabilities",
            Answers = _answers0_2,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers0_3 = new List<Answer>
        {
            new Answer(0,"to being"),new Answer(1,"being"),new Answer(2,"be"), new Answer(3,"on being", true)
        };
        private static Question _question03 = new Question
        {
            QuestionId = 4,
            TestId = 1,
            QuestionText = "Many Middle Eastern diplomats still feel that the USA is intent ... the ultimate policeman in the region.",
            Answers = _answers0_3,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers0_4 = new List<Answer>
        {
            new Answer(0,"to finish"),new Answer(1,"finish", true),new Answer(2,"finishing"), new Answer(3,"will have finished")
        };
        private static Question _question04 = new Question
        {
            QuestionId = 5,
            TestId = 1,
            QuestionText = "Woodrow Wilson believed the United States' entry into World War I would ... the war in months",
            Answers = _answers0_4,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers0_5 = new List<Answer>
        {
            new Answer(0,"The complete"),new Answer(1,"Completing"),new Answer(2,"A completing"), new Answer(3,"The completion", true)
        };
        private static Question _question05 = new Question
        {
            QuestionId = 6,
            TestId = 1,
            QuestionText = "... of New York's Erie Canal greatly enhanced trade in the upstate region",
            Answers = _answers0_5,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers0_6 = new List<Answer>
        {
            new Answer(0,"it attaches to", true),new Answer(1,"attaching to"),new Answer(2,"its attaching to"), new Answer(3,"where it attaches to")
        };
        private static Question _question06 = new Question
        {
            QuestionId = 7,
            TestId = 1,
            QuestionText = "After ... the skin, a leech is best removed by the application of either salt or heat.",
            Answers = _answers0_6,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static Test _englishTest0 = new Test
        {
            TestId = 1,
            TestName = "Pre-Intermediate",
            Questions = new Question[]
                                    {
                                        _question00, _question01, _question02, _question03, _question04, _question05, _question06
                                    }
        };

        private static List<Answer> _answers1_0 = new List<Answer>
        {
            new Answer(0,"Indigo was grown usually"),new Answer(1,"Usually grown was Indigo"),new Answer(2,"Indigo usually grown"), new Answer(3,"Indigo was usually grown", true)
        };
        private static Question _question10 = new Question
        {
            QuestionId = 8,
            TestId = 2,
            QuestionText = "... east of the Mississippi River.",
            Answers = _answers1_0,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };

        private static List<Answer> _answers1_1 = new List<Answer>
        {
            new Answer(0,"That was Victor Herbert who"),new Answer(1,"Victor Herbert who"),new Answer(2,"Since it was Victor Herbert"), new Answer(3,"It was Victor Herbert who", true)
        };
        private static Question _question11 = new Question
        {
            QuestionId = 9,
            TestId = 2,
            QuestionText = "... wrote the operetta \"Babes in Toyland\", drawn from the childhood characters of Mother Goose",
            Answers = _answers1_1,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_2 = new List<Answer>
        {
            new Answer(0,"those", true),new Answer(1,"them"),new Answer(2,"they"), new Answer(3,"their")
        };
        private static Question _question12 = new Question
        {
            QuestionId = 10,
            TestId = 2,
            QuestionText = "Some of the oldest and most widespread creation myths are ... involving the \"Earth Mother\"",
            Answers = _answers1_2,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_3 = new List<Answer>
        {
            new Answer(0,"the decade from"),new Answer(1,"the decade since"),new Answer(2,"the past decade", true), new Answer(3,"decade ago the")
        };
        private static Question _question13 = new Question
        {
            QuestionId = 11,
            TestId = 2,
            QuestionText = "In ... , compact disk technology has almost made record albums obsolete.",
            Answers = _answers1_3,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_4 = new List<Answer>
        {
            new Answer(0,"how its parents to recognize"),new Answer(1,"how to recognize its parents", true),new Answer(2,"to be recognizing its parents"), new Answer(3,"the recognizing of its parents")
        };
        private static Question _question14 = new Question
        {
            QuestionId = 12,
            TestId = 2,
            QuestionText = ". In the first few months of life, an infant learns how to lift its hands, how to smile and ... ",
            Answers = _answers1_4,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_5 = new List<Answer>
        {
            new Answer(0,"considered"),new Answer(1,"considered to be"),new Answer(2,"is considered to be", true), new Answer(3,"is consideration")
        };
        private static Question _question15 = new Question
        {
            QuestionId = 13,
            TestId = 2,
            QuestionText = "Juana Inez de la Cruz ... Mexico's greatest female poet",
            Answers = _answers1_5,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };


        private static List<Answer> _answers1_6 = new List<Answer>
        {
            new Answer(0,"is expanding", true),new Answer(1,"expands"),new Answer(2,"is expanded"), new Answer(3,"expanded")
        };
        private static Question _question16 = new Question
        {
            QuestionId = 14,
            TestId = 2,
            QuestionText = "Because the metal mercury ... in direct proportion to temperature, it was once used as the indicator in common thermometers",
            Answers = _answers1_6,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_7 = new List<Answer>
        {
            new Answer(0,"He reached"),new Answer(1,"When did he reach"),new Answer(2,"Having reached", true), new Answer(3,"Whether he reached")
        };
        private static Question _question17 = new Question
        {
            QuestionId = 15,
            TestId = 2,
            QuestionText = "... what is now San Salvador, Christopher Columbus believed that he had found Japan.",
            Answers = _answers1_7,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_8 = new List<Answer>
        {
            new Answer(0,"to study the stress experienced", true),new Answer(1,"study the experienced stress"),new Answer(2,"to study stress experiencing"), new Answer(3,"studying the stress experience")
        };
        private static Question _question18 = new Question
        {
            QuestionId = 16,
            TestId = 2,
            QuestionText = "The principal purpose of aviation medicine is ... by people aboard an aircraft in flight.",
            Answers = _answers1_8,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose write answer from a to d"
        };
        private static List<Answer> _answers1_9 = new List<Answer>
        {
            new Answer(0,"are"),new Answer(1,"call", true),new Answer(2,"because of"), new Answer(3,"males")
        };
        private static Question _question19 = new Question
        {
            QuestionId = 17,
            TestId = 2,
            QuestionText = " Guppies <i>are</i> sometimes <i>call</i> rainbow fish <i>because of</i> the <i>males</i>' bright colors.",
            Answers = _answers1_9,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose wrong part from words in italic a-d"
        };
        private static List<Answer> _answers1_10 = new List<Answer>
        {
            new Answer(0,"grown"),new Answer(1,"bears fruit"),new Answer(2,"than"), new Answer(3,"high", true)
        };
        private static Question _question1_10 = new Question
        {
            QuestionId = 18,
            TestId = 2,
            QuestionText = "The dwarf lemon tree, <i>grown</i> in many areas of the world, <i>bears fruit</i> when it is less <i>than</i> six inches in <i>high</i>.",
            Answers = _answers1_10,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose wrong part from words in italic a-d"
        };
        private static List<Answer> _answers1_11 = new List<Answer>
        {
            new Answer(0,"The"),new Answer(1,"softly", true),new Answer(2,"gray matter"), new Answer(3,"that")
        };
        private static Question _question1_11 = new Question
        {
            QuestionId = 19,
            TestId = 2,
            QuestionText = "<i>The</i> brain is composed of a mass of <i>softly</i> <i>gray matter</i> in the skull <i>that</i> controls our intelligence.",
            Answers = _answers1_11,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose wrong part from words in italic a-d"
        };
        private static List<Answer> _answers1_12 = new List<Answer>
        {
            new Answer(0,"Polluter", true),new Answer(1,"importance"),new Answer(2,"well-informed"), new Answer(3,"its")
        };
        private static Question _question1_12 = new Question
        {
            QuestionId = 20,
            TestId = 2,
            QuestionText = "<i>Polluter</i> is a topic of such <i>importance</i> today that even elementary school children are <i>well-informed</i> about <i>its</i> danger",
            Answers = _answers1_12,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose wrong part from words in italic a-d"
        };
        private static List<Answer> _answers1_13 = new List<Answer>
        {
            new Answer(0,"Best"),new Answer(1,"oil painting"),new Answer(2,"it", true), new Answer(3,"of the history")
        };
        private static Question _question1_13 = new Question
        {
            QuestionId = 21,
            TestId = 2,
            QuestionText = "<i>Best</i> represented in a famous <i>oil painting</i> by Da Vinci, The Last Supper <i>it</i> is an important part <i>of the history</i> of Christianity",
            Answers = _answers1_13,
            QuestionType = QuestionType.SingleAnswer,
            QuestionDescription = "Choose wrong part from words in italic a-d"
        };
        private static Test _englishTest1 = new Test
        {
            TestId = 2,
            TestName = "Intermediate",
            Questions = new Question[]
                                    {
                                        _question10, _question11, _question12, _question13, _question14, _question15, _question16,
                                        _question17, _question18, _question19, _question1_10, _question1_11, _question1_12, _question1_13
                                    }
        };

        private List<Test> _allTests = new List<Test>
        {
            _englishTest0, _englishTest1
        };
        private static Role student = new Role
        {
            RoleId = 1,
            RoleName = "Student"
        };
        private static Role teacher = new Role
        {
            RoleId = 2,
            RoleName = "Teacher"
        };
        private static Role reviewer = new Role
        {
            RoleId = 3,
            RoleName = "Reviewer"
        };
        private static Role admin = new Role
        {
            RoleId = 4,
            RoleName = "Admin"
        };
        private List<Role> _allRoles = new List<Role>
                                    {
                                        student, teacher, reviewer, admin
                                    };

        public TestRepository()
        {
        }

        public IEnumerable<Test> GetTests()
        {
            return _allTests;
        }

        public Test GetTest(int id)
        {
            return _allTests.SingleOrDefault(q => q.TestId == id);
        }

        public Test InsertTest(Test test)
        {
            if (_allTests.Any(t => t.TestName == test.TestName))
                throw new BusinessLogicException("Duplicate test name");
            test.TestId = _allTests.Any() ? _allTests.Max(t => t.TestId) + 1 : 1;
            _allTests.Add(test);
            return _allTests.Last();
        }

        public void UpdateTest(Test test)
        {
            var index = _allTests.FindIndex(t => t.TestId == test.TestId);
            if (index == -1)
                throw new BusinessLogicException("test not found");
            _allTests[index] = test;
        }

        public void DeleteTest(int id)
        {
            var test = _allTests.FirstOrDefault(t => t.TestId == id);
            if (test == null)
                throw new BusinessLogicException("Test not found");
            _allTests.Remove(test);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _allTests.SelectMany(t => t.Questions);
        }

        public IEnumerable<Question> GetQuestions(int testId)
        {
            var test = _allTests.SingleOrDefault(t => t.TestId == testId);
            return test != null ? test.Questions : null;
        }

        public Question GetQuestion(int id)
        {
            return _allTests.SelectMany(t => t.Questions).SingleOrDefault(q => q.QuestionId == id);
        }

        public Question InsertQuestion(Question question, int testId)
        {
            var test = _allTests.SingleOrDefault(t => t.TestId == testId);
            if (test == null)
                throw new BusinessLogicException("Test not found");
            question.TestId = testId;
            question.QuestionId = test.Questions.Any() ? test.Questions.Max(q => q.QuestionId) + 1 : 1;
            test.Questions = test.Questions.Union(new[] { question }).ToArray();
            return test.Questions.Last();
        }

        public void UpdateQuestion(Question newQuestion)
        {
            var question = _allTests.SelectMany(t => t.Questions).SingleOrDefault(q => q.QuestionId == newQuestion.QuestionId);
            if (question == null)
                throw new BusinessLogicException("Question not found");
            var test = _allTests.SingleOrDefault(t => t.TestId == question.TestId);
            if (test != null)
            {
                for (var index = 0; index < test.Questions.Length; index++)
                    if (test.Questions[index].QuestionId == newQuestion.QuestionId)
                    {
                        test.Questions[index] = newQuestion;
                        break;
                    }
            }
        }

        public IEnumerable<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public User InsertUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _allRoles;
        }

        public Role GetRole(int roleId)
        {
            return _allRoles.SingleOrDefault(r => r.RoleId == roleId);
        }

        public Role InsertRole(Role role)
        {
            _allRoles.Add(role);
            return _allRoles.Last();
        }

        public void UpdateRole(Role role)
        {
            var index = _allTests.FindIndex(t => t.TestId == role.RoleId);
            if (index != -1)
            {
                _allRoles[index] = role;
            }
            else
            {
                throw new BusinessLogicException("Role not found");
            }
        }

        public void DeleteQuestion(int questionId)
        {
            var question = _allTests.SelectMany(t => t.Questions).FirstOrDefault(t => t.QuestionId == questionId);
            if (question == null)
                throw new BusinessLogicException("Question not found");
            var test =             _allTests.Single(t => t.TestId == question.TestId);
            test.Questions = test.Questions.Where(q => q.QuestionId != questionId).ToArray();
        }

        public void DeleteRole(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}