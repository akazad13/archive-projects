using Microsoft.AspNetCore.Identity;


namespace WMC.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
