namespace EduTesting.Model
{
    public class Answer
    {
        public Answer(int answerPosition, string answerText)
        {
            AnswerPosition = answerPosition;
            AnswerText = answerText;
        }

        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public bool AnswerIsRight { get; set; }
        public string AnswerText { get; set; }
        public int AnswerPosition { get; set; }
    }
}