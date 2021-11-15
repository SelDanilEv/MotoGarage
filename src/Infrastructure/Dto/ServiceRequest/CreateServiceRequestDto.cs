using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dto.ServiceRequest
{
    public class CreateServiceRequestDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
