using System.Collections.Generic;

namespace CMS.Models.Dto
{
    public class QuizForAddDto
    {
        public int CourseId { get; set; }
        public string QuizName { get; set; }
        public List<QAndA> QuesAndAns { get; set; }
    }
}
