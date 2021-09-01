
using Bdaya.Responses;

internal class ApiResultWithMessages : IApiResult
{
    public ApiResultWithMessages()
    {
    }

    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
    public bool IsSuccess { get; set; }
    public bool IsFail => !IsSuccess;
}
