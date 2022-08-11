using System.Collections.Generic;

namespace CMS.Models.Dto
{
    public class CourseAndQuizList
    {
        public CourseAndQuizList()
        {
            Courses = new List<Course>();
            Quizzes = new List<Quiz>();
        }
        public List<Course> Courses { get; set; }
        public List<Quiz> Quizzes { get; set; }
    }
}
