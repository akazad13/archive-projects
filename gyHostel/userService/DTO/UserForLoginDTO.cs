using System.ComponentModel.DataAnnotations;

namespace userService.DTO
{
    public class UserForLoginDTO
    {
        [StringLength(45)]
        public string Account { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
    }
}
