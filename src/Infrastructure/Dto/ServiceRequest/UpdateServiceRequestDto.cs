using Infrastructure.Dto.Reviews;
using Infrastructure.Enums;
using Infrastructure.Models.Reviews;
using System;

namespace Infrastructure.Dto.ServiceRequest
{
    public class UpdateServiceRequestDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public Review Review { get; set; }

        public Guid ReporterId { get; set; }
        public Guid AssigneeId { get; set; }
    }
}
