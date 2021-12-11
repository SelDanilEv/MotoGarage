using Infrastructure.Dto.Reviews;
using Infrastructure.Enums;
using System;

namespace Infrastructure.Dto.ServiceRequest
{
    public class AddRequestReviewDto
    {
        public Guid Id { get; set; }

        public ReviewDto Review { get; set; }
    }
}
