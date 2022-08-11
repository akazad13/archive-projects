using WMC.Models;

namespace WMC.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Issue { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public string FileName { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
