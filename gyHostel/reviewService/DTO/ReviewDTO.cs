using System.ComponentModel.DataAnnotations;

namespace reviewService.DTO
{
    public class ReviewDTO
    {
        public int Rating { get; set; }

        [StringLength(45)]
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
