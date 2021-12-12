using System;

namespace Infrastructure.Dto.Reviews
{
    public class ReviewDisplayDto
    {
        public string ReviewText { get; set; }
        public string ClientName { get; set; }
        public int Rate { get; set; }
    }
}
