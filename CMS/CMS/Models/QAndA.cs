namespace CMS.Models
{
    public class QAndA
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }

        public int QuizId { get; set; }
        public Quiz Quizzes { get; set; }
    }
}
