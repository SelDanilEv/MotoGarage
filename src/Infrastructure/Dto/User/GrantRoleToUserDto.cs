using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class GrantRoleToUserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
