using Infrastructure.ResponseModels;

namespace Infrastructure.Result.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }

        ResultStatus Status { get; }

        string Message { get; set; }

        ErrorResponse GetErrorResponse { get; }
    }
}
