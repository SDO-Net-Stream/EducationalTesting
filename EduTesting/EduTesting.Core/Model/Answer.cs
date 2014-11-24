namespace EduTesting.Model
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public bool AnswerIsRight { get; set; }
        public string AnswerText { get; set; }
        public int AnswerOrder { get; set; }
    }
}