namespace TakeItToTheCloud.Models.Dto
{
    public class UploadedFileDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? UploadedTime { get; set; }
    }
}
