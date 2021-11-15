using Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.ServiceRequest
{
    public class ChangeRequestStatusDto
    {
        [Required]
        public Guid RequestId { get; set; }

        [Required]
        public ServiceRequestStatus NewStatus { get; set; }
    }
}
