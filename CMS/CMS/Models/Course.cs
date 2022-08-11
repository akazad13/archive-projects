using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Course Name should be minimum 3 characters and a maximum of 50 characters")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Maximum of 200 characters allowed for course description")]
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Quiz> Quizzes { get; set; }
    }
}
