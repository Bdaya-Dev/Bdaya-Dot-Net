using Bdaya.Responses;

namespace System;

public static class ObjectExt
{
    public static Task<T> AsTask<T>(this T obj)
    {
        return Task.FromResult(obj);
    }

    public static IApiResult<T> AsSuccess<T>(this T obj, params string[] messages)
    {
        if (messages == null || messages.Length == 0)
        {
            return IApiResult<T>.Success(obj);
        }
        else
        {
            return IApiResult<T>.Success(obj, messages.ToList());
        }
    }
    public static IApiResult<T> AsFail<T>(this T obj, params string[] messages)
    {
        if (messages == null || messages.Length == 0)
        {
            return IApiResult<T>.Success(obj);
        }
        else
        {
            return IApiResult<T>.Success(obj, messages.ToList());
        }
    }
}
