using System;

namespace Infrastructure.Dto.Reviews
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string ReviewText { get; set; }
        public int Rate { get; set; }
    }
}
