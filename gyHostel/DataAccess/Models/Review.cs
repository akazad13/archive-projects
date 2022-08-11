using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Review
    {
        [Key]
        public int Review_Id { get; set; }
        public int Rating { get; set; }

        [StringLength(45)]
        public string Content { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
