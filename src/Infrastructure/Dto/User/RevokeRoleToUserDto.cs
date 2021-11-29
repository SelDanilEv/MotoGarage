using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class RevokeRoleToUserDto
    {
        [Required]
        public string Id { get; set; }
    }
}
