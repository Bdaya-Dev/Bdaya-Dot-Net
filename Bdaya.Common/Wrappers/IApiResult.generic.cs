namespace Bdaya.Responses;

public interface IApiResult<out T> : IApiResult
{
    T? Data { get; }

    public new static IApiResult<T> Fail()
    {
        return new ApiResultWithMessages<T> { IsSuccess = false };
    }
    public new static IApiResult<T> Fail(string message)
    {
        return new ApiResultWithMessages<T> { IsSuccess = false, Messages = new List<string> { message } };
    }

    public new static IApiResult<T> Fail(IReadOnlyList<string> messages)
    {
        return new ApiResultWithMessages<T> { IsSuccess = false, Messages = messages };
    }


    public new static IApiResult<T> Success()
    {
        return new ApiResultWithMessages<T> { IsSuccess = true };
    }

    public new static IApiResult<T> Success(string message)
    {
        return new ApiResultWithMessages<T> { IsSuccess = true, Messages = new List<string> { message } };
    }

    public static IApiResult<T> Success(T data)
    {
        return new ApiResultWithMessages<T> { IsSuccess = true, Data = data };
    }

    public static IApiResult<T> Success(T data, string message)
    {
        return new ApiResultWithMessages<T> { IsSuccess = true, Data = data, Messages = new List<string> { message } };
    }

    public static IApiResult<T> Success(T data, IReadOnlyList<string> messages)
    {
        return new ApiResultWithMessages<T> { IsSuccess = true, Data = data, Messages = messages };
    }
}
