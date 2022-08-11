using Microsoft.AspNetCore.Identity;

namespace WMC.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string? CompanyName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Consultation> Consultations { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
