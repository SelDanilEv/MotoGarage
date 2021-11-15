namespace Infrastructure.Result.Interfaces
{
    public interface IResultWithData<T> : IResult
    {
        T GetData { get; }
    }
}
