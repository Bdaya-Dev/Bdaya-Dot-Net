namespace System;

public static class TaskExt
{
    public static Task<TTarget?> Then<TSrc, TTarget>(this Task<TSrc> task, Func<TSrc, TTarget> onSuccess, Func<TSrc?, TTarget?>? onFail = default)
    {
        return task.ContinueWith((t) =>
        {
            if (t.IsCompletedSuccessfully)
            {
                return onSuccess(t.Result);
            }
            else
            {
                if (onFail == null) return default;
                return onFail(t.Result);
            }
        });
    }
}
