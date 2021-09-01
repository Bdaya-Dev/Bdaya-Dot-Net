using Bdaya.Responses;

internal class ApiResultWithMessages<T> : ApiResultWithMessages, IApiResult<T>
{
    public ApiResultWithMessages()
    {
    }

    public T? Data { get; set; }
}
