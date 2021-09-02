using Bdaya.Responses;

public class ApiResult<T> : ApiResult
{

    public T? Data { get; set; }

    public static implicit operator T?(ApiResult<T> res)
    {
        return res.Data;
    }
    public static implicit operator ApiResult<T>(T inp)
    {
        return ApiResult<T>.Success(inp);
    }
    public new static ApiResult<T> Fail()
    {
        return new ApiResult<T> { IsSuccess = false };
    }
    public new static ApiResult<T> Fail(string message)
    {
        return new ApiResult<T> { IsSuccess = false, Messages = new List<string> { message } };
    }

    public new static ApiResult<T> Fail(IReadOnlyList<string> messages)
    {
        return new ApiResult<T> { IsSuccess = false, Messages = messages };
    }


    public new static ApiResult<T> Success()
    {
        return new ApiResult<T> { IsSuccess = true };
    }

    public new static ApiResult<T> Success(string message)
    {
        return new ApiResult<T> { IsSuccess = true, Messages = new List<string> { message } };
    }

    public static ApiResult<T> Success(T data)
    {
        return new ApiResult<T> { IsSuccess = true, Data = data };
    }

    public static ApiResult<T> Success(T data, string message)
    {
        return new ApiResult<T> { IsSuccess = true, Data = data, Messages = new List<string> { message } };
    }

    public static ApiResult<T> Success(T data, IReadOnlyList<string> messages)
    {
        return new ApiResult<T> { IsSuccess = true, Data = data, Messages = messages };
    }
}
