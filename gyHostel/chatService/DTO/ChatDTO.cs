using System.ComponentModel.DataAnnotations;

namespace chatService.DTO
{
    public class ChatDTO
    {
        [StringLength(45)]
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
