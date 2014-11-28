namespace EduTesting.Model
{
    public class Answer
    {
        public Answer(int answerPosition, string answerText, bool isRight = false)
        {
            AnswerId = answerPosition;
            AnswerPosition = answerPosition;
            AnswerText = answerText;
            AnswerIsRight = isRight;
        }

        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public bool AnswerIsRight { get; set; }
        public string AnswerText { get; set; }
        public int AnswerPosition { get; set; }
    }
}