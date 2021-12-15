using Infrastructure.Models.Identity;

namespace Infrastructure.Models.User
{
    public class UpdateUserInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
