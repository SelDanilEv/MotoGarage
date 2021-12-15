using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.ResetPassword
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
