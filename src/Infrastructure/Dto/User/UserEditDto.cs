using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class UserEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
