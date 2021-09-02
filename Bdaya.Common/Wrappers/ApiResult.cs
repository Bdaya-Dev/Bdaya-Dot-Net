
using Bdaya.Responses;

public class ApiResult
{
    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
    public bool IsSuccess { get; set; }
    public bool IsFail => !IsSuccess;
    public string Message => string.Join('\n', Messages);

    public static ApiResult Fail()
    {
        return new ApiResult { IsSuccess = false };
    }

    public static ApiResult Fail(string message)
    {
        return new ApiResult { IsSuccess = false, Messages = new List<string> { message } };
    }

    public static ApiResult Fail(IReadOnlyList<string> messages)
    {
        return new ApiResult { IsSuccess = false, Messages = messages };
    }

    public static ApiResult Success()
    {
        return new ApiResult { IsSuccess = true };
    }

    public static ApiResult Success(string message)
    {
        return new ApiResult { IsSuccess = true, Messages = new List<string> { message } };
    }
    public static ApiResult Success(IReadOnlyList<string> messages)
    {
        return new ApiResult { IsSuccess = true, Messages = messages };
    }
}
