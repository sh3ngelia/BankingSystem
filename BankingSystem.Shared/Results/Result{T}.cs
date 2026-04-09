namespace BankingSystem.Shared.Results;

public class Result<T> : Result
{
    private Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    public T Value { get; }

    public static Result<T> Success(T value) => new(value, true, string.Empty);
    public static new Result<T> Failure(string error) => new(default!, false, error);
}
