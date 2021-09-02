using Bdaya.Responses;

namespace System;

public static class ObjectExt
{
    public static Task<T> AsTask<T>(this T obj)
    {
        return Task.FromResult(obj);
    }

    public static ApiResult<T> AsSuccess<T>(this T obj, params string[] messages)
    {
        if (messages == null || messages.Length == 0)
        {
            return ApiResult<T>.Success(obj);
        }
        else
        {
            return ApiResult<T>.Success(obj, messages.ToList());
        }
    }
    public static ApiResult<T> AsFail<T>(this T obj, params string[] messages)
    {
        if (messages == null || messages.Length == 0)
        {
            return ApiResult<T>.Success(obj);
        }
        else
        {
            return ApiResult<T>.Success(obj, messages.ToList());
        }
    }
}
