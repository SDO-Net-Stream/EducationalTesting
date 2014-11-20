using System.Collections.Generic;
using System.Linq;
using EduTesting.Controllers;
using EduTesting.Model;

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
    private static List<string> _answers0_0 = new List<string>
    {
       
      "another pants","other pants","the other ones","another pair"
    };
    private static Question _question00 = new Question
    {
      QuestionId = 1,
      QuestionText = "Because the first pair of shoes did not fit properly, he asked for ... .",
      Answers = _answers0_0,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
     
    private static List<string> _answers0_1 = new List<string>
    {
      "However","Yet","That","Although"
    };
    private static Question _question01 = new Question
    {
      QuestionId = 2,
      QuestionText = "... the Boston Red Sox have often been outstanding, they haven’t won the World Series since 1918.",
      Answers = _answers0_1,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };

    private static List<string> _answers0_2 = new List<string>
    {
       "There are","The","There is a lot of","Some"
    };
    private static Question _question02 = new Question
    {
      QuestionId = 3,
      QuestionText = ". ... many computer software programs that possess excellent word-processing capabilities",
      Answers = _answers0_2,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers0_3 = new List<string>
    {
       "to being","being","be","on being"
    };
    private static Question _question03 = new Question
    {
      QuestionId = 4,
      QuestionText = "Many Middle Eastern diplomats still feel that the USA is intent ... the ultimate policeman in the region.",
      Answers = _answers0_3,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers0_4 = new List<string>
    {
       "to finish","finish","finishing","will have finished"
    };
    private static Question _question04 = new Question
    {
      QuestionId = 5,
      QuestionText = "Woodrow Wilson believed the United States' entry into World War I would ... the war in months",
      Answers = _answers0_4,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers0_5 = new List<string>
    {
       "The complete","Completing","A completing","The completion"
    };
    private static Question _question05 = new Question
    {
      QuestionId = 6,
      QuestionText = "... of New York's Erie Canal greatly enhanced trade in the upstate region",
      Answers = _answers0_5,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers0_6 = new List<string>
    {
      "it attaches to","attaching to","its attaching to","where it attaches to"
    };
    private static Question _question06 = new Question
    {
      QuestionId = 7,
      QuestionText = "After ... the skin, a leech is best removed by the application of either salt or heat.",
      Answers = _answers0_6,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private Test _englishTest0 = new Test{
                                  Questions = new List<IQuestion>
                                  {
                                    _question01, _question01, _question02, _question03, _question04, _question05, _question06
                                  }
                                };
    
    

    private static List<string> _answers1_0 = new List<string>
    {
      "Indigo was grown usually","Usually grown was Indigo","Indigo usually grown","Indigo was usually grown"
    };
    private static Question _question10 = new Question
    {
      QuestionId = 8,
      QuestionText = "... east of the Mississippi River.",
      Answers = _answers1_0,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };

    private static List<string> _answers1_1 = new List<string>
    {
       "That was Victor Herbert who","Victor Herbert who","Since it was Victor Herbert","It was Victor Herbert who"
    };
    private static Question _question11 = new Question
    {
      QuestionId = 9,
      QuestionText = "... wrote the operetta \"Babes in Toyland\", drawn from the childhood characters of Mother Goose",
      Answers = _answers1_1,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_2 = new List<string>
    {
       "those","them","they","their"
    };
    private static Question _question12 = new Question
    {
      QuestionId = 10,
      QuestionText = "Some of the oldest and most widespread creation myths are ... involving the \"Earth Mother\"",
      Answers = _answers1_2,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_3 = new List<string>
    {
       "the decade from","the decade since","the past decade","decade ago the"
    };
    private static Question _question13 = new Question
    {
      QuestionId = 11,
      QuestionText = "In ... , compact disk technology has almost made record albums obsolete.",
      Answers = _answers1_3,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_4 = new List<string>
    {
       "how its parents to recognize","how to recognize its parents","to be recognizing its parents","the recognizing of its parents"
    };
    private static Question _question14 = new Question
    {
      QuestionId = 12,
      QuestionText = ". In the first few months of life, an infant learns how to lift its hands, how to smile and ... ",
      Answers = _answers1_4,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_5 = new List<string>
    {
       "considered","considered to be","is considered to be","is consideration"
    };
    private static Question _question15 = new Question
    {
      QuestionId = 13,
      QuestionText = "Juana Inez de la Cruz ... Mexico's greatest female poet",
      Answers = _answers1_5,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };


    private static List<string> _answers1_6 = new List<string>
    {
       "is expanding","expands","is expanded","expanded"
    };
    private static Question _question16 = new Question
    {
      QuestionId = 14,
      QuestionText = "Because the metal mercury ... in direct proportion to temperature, it was once used as the indicator in common thermometers",
      Answers = _answers1_6,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_7 = new List<string>
    {
       "He reached","When did he reach","Having reached","Whether he reached"
    };
    private static Question _question17 = new Question
    {
      QuestionId = 15,
      QuestionText = "... what is now San Salvador, Christopher Columbus believed that he had found Japan.",
      Answers = _answers1_7,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_8 = new List<string>
    {
       "to study the stress experienced","study the experienced stress","to study stress experiencing","studying the stress experience"
    };
    private static Question _question18 = new Question
    {
      QuestionId = 16,
      QuestionText = "The principal purpose of aviation medicine is ... by people aboard an aircraft in flight.",
      Answers = _answers1_8,
      TestType = "Radio-type",
      Description = "Choose write answer from a to d"
    };
    private static List<string> _answers1_9 = new List<string>
    {
       "are","call","because of","males"
    };
    private static Question _question19 = new Question
    {
      QuestionId = 17,
      QuestionText = " Guppies <i>are</i> sometimes <i>call</i> rainbow fish <i>because of</i> the <i>males</i>' bright colors.",
      Answers = _answers1_9,
      TestType = "Radio-type",
      Description = "Choose wrong part from words in italic a-d"
    };
    private static List<string> _answers1_10 = new List<string>
    {
       "grown","bears fruit","than","high"
    };
    private static Question _question1_10 = new Question
    {
      QuestionId = 18,
      QuestionText = "The dwarf lemon tree, <i>grown</i> in many areas of the world, <i>bears fruit</i> when it is less <i>than</i> six inches in <i>high</i>.",
      Answers = _answers1_10,
      TestType = "Radio-type",
      Description = "Choose wrong part from words in italic a-d"
    };
    private static List<string> _answers1_11 = new List<string>
    {
       "The","softly","gray matter","that"
    };
    private static IQuestion _question1_11 = new FixedQuestion
    {
      QuestionId = 19,
      QuestionText = "<i>The</i> brain is composed of a mass of <i>softly</i> <i>gray matter</i> in the skull <i>that</i> controls our intelligence.",
      Answers = _answers1_11,
      TestType = "Radio-type",
      Description = "Choose wrong part from words in italic a-d"
    };
    private static List<string> _answers1_12 = new List<string>
    {
       "Polluter","importance","well-informed","its"
    };
    private static Question _question1_12 = new Question
    {
      QuestionId = 20,
      QuestionText = "<i>Polluter</i> is a topic of such <i>importance</i> today that even elementary school children are <i>well-informed</i> about <i>its</i> danger",
      Answers = _answers1_12,
      TestType = "Radio-type",
      Description = "Choose wrong part from words in italic a-d"
    };
    private static List<string> _answers1_13 = new List<string>
    {
       "Best","oil painting","it","of the history"
    };
    private static Question _question1_13 = new Question
    {
      QuestionId = 21,
      QuestionText = "<i>Best</i> represented in a famous <i>oil painting</i> by Da Vinci, The Last Supper <i>it</i> is an important part <i>of the history</i> of Christianity",
      Answers = _answers1_13,
      TestType = "Radio-type",
      Description = "Choose wrong part from words in italic a-d"
    };
    private Test _englishTest1 = new Test{
                                  Questions = new List<IQuestion>
                                  {
                                    _question10, _question11, _question12, _question13, _question14, _question15, _question16,
                                    _question17, _question18, _question19, _question1_10, _question1_11, _question1_12, _question1_13
                                  }
                                };

    private List<IQuestion> _allQuestions = new List<IQuestion>
                                  {
                                    _question01, _question01, _question02, _question03, _question04, _question05, _question06,
                                    _question10, _question11, _question12, _question13, _question14, _question15, _question16,
                                    _question17, _question18, _question19, _question1_10, _question1_11, _question1_12, _question1_13
                                  };

    private static Role student = new Role
    {
      RoleName = "Student"
    };
    private static Role teacher = new Role
    {
      RoleName = "Teacher"
    };
    private static Role reviewer = new Role
    {
      RoleName = "Reviewer"
    };
    private static Role admin = new Role
    {
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
      var listOfTests = new List<Test>
      {
        _englishTest0, _englishTest1
      };
      return listOfTests;
    }

    public Test GetTest(int id)
    {
      switch (id)
      {
        case 1:
        {
          return _englishTest0;
        }
        case 2:
        {
          return _englishTest1;
        }
        default:
        return _englishTest0;
      }
    }

    public Test InsertTest(Test test)
    {
      throw new System.NotImplementedException();
    }

    public bool UpdateTest(Test test)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<IQuestion> GetAllQuestions()
    {
      return _allQuestions;
    }

    public IEnumerable<IQuestion> GetQuestions(int testId)
    {
      switch (testId)
      {
        case 1:
          return _englishTest0.Questions;
        case 2:
          return _englishTest1.Questions;
        default:
          return null;
      }
    }

    public IQuestion GetQuestion(int id)
    {
      return _allQuestions.SingleOrDefault(q => q.QuestionId == id);
    }

    public IQuestion InsertQuestion(IQuestion question)
    {
      throw new System.NotImplementedException();
    }

    public bool UpdateQuestion(IQuestion question)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<User> GetUsers()
    {
      throw new System.NotImplementedException();
    }

    public User GetUser(int id)
    {
      throw new System.NotImplementedException();
    }

    public bool UpdateUser(User user)
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

    public Role GetRole(int id)
    {
      throw new System.NotImplementedException();
    }

    public Role InsertRole(Role Role)
    {
      throw new System.NotImplementedException();
    }

    public bool UpdateRole(Role role)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteTest(int id)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteRole(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}