namespace TakeItToTheCloud.Models.Dto
{
    public class FileUploadDto
    {
        public int Id { get; set; }
        public IFormCollection Doc { get; set; }
    }
}
