using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.ResetPassword
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Code { get; set; }
    }
}
