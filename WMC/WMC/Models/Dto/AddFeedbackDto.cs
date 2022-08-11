namespace WMC.Models.Dto
{
    public class AddFeedbackDto
    {
        public DateTime FeedbackDate { get; set; }
        public string Description { get; set; }
        public string ServiceName { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
    }
}
