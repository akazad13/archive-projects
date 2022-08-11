using System.Collections.Generic;

namespace CMS.Models.Dto
{
    public class ExamSubmissionDto
    {
        public int CourseId { get; set; }
        public int QuizId { get; set; }
        public List<ExamPaper> ExamPaper { get; set; } = new List<ExamPaper>();
    }

    public class ExamPaper
    {
        public int QuestionId { get; set; }
        public int SelectedOptionNo { get; set; }
    }
}
