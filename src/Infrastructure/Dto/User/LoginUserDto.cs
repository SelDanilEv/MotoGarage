using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class LoginUserDto
    {
        [Required]
        public string LoginOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
