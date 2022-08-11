namespace WMC.Models.Dto
{
    public class AddConsultationDto
    {
        public string ServiceName { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Issue { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public IFormCollection Doc { get; set; }
    }
}
