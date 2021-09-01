using System.Collections.Generic;

namespace Bdaya.Responses;

public interface IApiResult
{
    IReadOnlyList<string> Messages { get; set; }
    bool IsSuccess { get; set; }
    public static IApiResult Fail()
    {
        return new ApiResultWithMessages { IsSuccess = false };
    }

    public static IApiResult Fail(string message)
    {
        return new ApiResultWithMessages { IsSuccess = false, Messages = new List<string> { message } };
    }

    public static IApiResult Fail(IReadOnlyList<string> messages)
    {
        return new ApiResultWithMessages { IsSuccess = false, Messages = messages };
    }

    public static IApiResult Success()
    {
        return new ApiResultWithMessages { IsSuccess = true };
    }

    public static IApiResult Success(string message)
    {
        return new ApiResultWithMessages { IsSuccess = true, Messages = new List<string> { message } };
    }
    public static IApiResult Success(IReadOnlyList<string> messages)
    {
        return new ApiResultWithMessages { IsSuccess = true, Messages = messages };
    }
}
