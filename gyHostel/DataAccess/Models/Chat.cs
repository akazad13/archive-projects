using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Chat
    {
        [Key]
        public int Chat_Id { get; set; }

        [StringLength(45)]
        public string Content { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
