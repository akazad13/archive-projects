using Microsoft.AspNetCore.Identity;

namespace TakeItToTheCloud.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? About { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; }
    }
}
