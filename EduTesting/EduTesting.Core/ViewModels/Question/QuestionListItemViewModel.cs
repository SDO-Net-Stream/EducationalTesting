namespace EduTesting.ViewModels.Question
{
    public class QuestionListItemViewModel
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public EduTesting.ViewModels.TestResult.QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string QuestionDescription { get; set; }
    }
}
