namespace WMC.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string Description { get; set; }
        public string ServiceName { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
