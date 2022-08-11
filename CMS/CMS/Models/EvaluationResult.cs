using System.Collections.Generic;

namespace CMS.Models
{
    public class EvaluationResult
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int QuizId { get; set; }
        public Quiz Quizzes { get; set; }

        public int TotalQuestions { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
