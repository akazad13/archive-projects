namespace TakeItToTheCloud.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? UploadedTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
