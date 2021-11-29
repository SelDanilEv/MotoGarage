using Infrastructure.Enums;
using System.Collections.Generic;

namespace Infrastructure.ResponseModels
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public string Title { get; set; }
        public int Status { get; set; } = 400;
        public Dictionary<string,string[]> Errors { get; set; }
        public ErrorResponseType Type { get; set; }
    }
}
