using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.User
{
    public class UpdateUserInfoDto
    {
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
