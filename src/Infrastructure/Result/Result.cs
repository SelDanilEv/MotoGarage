using Infrastructure.ResponseModels;
using Infrastructure.Result;
using Infrastructure.Result.Interfaces;
using System.Linq;

namespace Infrastructure.Result
{
    public class Result<T> : IResultWithData<T>
    {
        public Result()
        {
            Status = ResultStatus.Success;
            ErrorResponse = new ErrorResponse();
        }
        
        public Result(IResult result)
        {
            Status = result.Status;
            Message = result.Message;
            ErrorResponse = result.GetErrorResponse;
        }

        public Result(T data) : this()
        {
            Data = data;
        }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public ErrorResponse ErrorResponse { get; set; }
        public ErrorResponse GetErrorResponse => IsSuccess ? default  : ErrorResponse;

        public Result<T> AddError(string field, string message)
        {
            ErrorResponse.Errors.Add(field, new[] { message });
            return this;
        }

        public T Data { private get; set; }
        public T GetData => Data;

        public bool IsSuccess => Status != ResultStatus.Error;

        public static Result<T> SuccessResult() => new Result<T>() { Status = ResultStatus.Success };
        public static Result<T> WarningResult() => new Result<T>() { Status = ResultStatus.Warning };
        public static Result<T> ErrorResult() => new Result<T>() { Status = ResultStatus.Error };

        public static Result<T> SuccessResult(IResult result) => new Result<T>(result) { Status = ResultStatus.Success };
        public static Result<T> WarningResult(IResult result) => new Result<T>(result) { Status = ResultStatus.Warning };
        public static Result<T> ErrorResult(IResult result) => new Result<T>(result) { Status = ResultStatus.Error };

        public static Result<T> SuccessResult(T data) => new Result<T>() { Status = ResultStatus.Success, Data = data };
        public static Result<T> ErrorResult(string errMessage) => new Result<T>() { Status = ResultStatus.Error, Message = errMessage };

        public Result<T> BuildMessage(params string[] messages)
        {
            var template = messages[0];
            messages = messages.Skip(1).ToArray();
            Message = string.Format(template, messages);
            return this;
        }

        public Result<T> AppendMessage(params string[] messages)
        {
            var template = messages[0];
            messages = messages.Skip(1).ToArray();
            Message += string.Format(template, messages);
            return this;
        }
    }

    public class Result : Result<string>
    {

    }
}
