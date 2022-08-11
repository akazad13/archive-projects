using Microsoft.AspNetCore.Identity;


namespace TakeItToTheCloud.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
