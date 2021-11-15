using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class RoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
