using Infrastructure.Dto.Reviews;
using Infrastructure.Dto.User;
using Infrastructure.Enums;
using System;

namespace Infrastructure.Dto.ServiceRequest
{
    public class ServiceRequestDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public ReviewDto Review { get; set; }

        public UserDto Reporter { get; set; }
        public UserDto Assignee { get; set; }
    }
}
