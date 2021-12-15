using Infrastructure.Models.Identity;

namespace Infrastructure.Models.ResetPassword
{
    public class UpdatePassword
    {
        public ApplicationUser User { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
