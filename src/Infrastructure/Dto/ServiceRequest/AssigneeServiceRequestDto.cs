using Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.ServiceRequest
{
    public class AssigneeServiceRequestDto
    {
        [Required]
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
    }
}
