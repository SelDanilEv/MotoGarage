using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
