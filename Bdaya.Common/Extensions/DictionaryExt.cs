using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections;

public static class DictionaryExt
{
    public static TSelf ConcatWith<TSelf, TKey, TVal>(
        this TSelf src,
        IEnumerable<IReadOnlyDictionary<TKey, TVal>> others,
        bool modifySource = true
    ) where TKey : notnull
        where TSelf : IDictionary<TKey, TVal>
    {
        var target = modifySource ? src : new Dictionary<TKey, TVal>(src) as IDictionary<TKey, TVal>;
        foreach (var item in others)
        {
            foreach (var pair in item)
            {
                target[pair.Key] = pair.Value;
            }
        }
        return (TSelf)target;
    }

    public static TSelf ConcatWith<TSelf, TKey, TVal>(
        this TSelf src,
        params IReadOnlyDictionary<TKey, TVal>[] others
    ) where TKey : notnull
        where TSelf : IDictionary<TKey, TVal>
    {
        return ConcatWith(src, others.AsEnumerable());
    }

    public static bool TryGetValueCasted<TKey, TVal, TCast>(
        this IReadOnlyDictionary<TKey, TVal>? src,
        TKey? key,
        [NotNullWhen(true)] out TCast? value
    )
    {
        if (key is null || src is null)
        {
            value = default;
            return false;
        }
        if (src.TryGetValue(key, out var middle) && middle is TCast resV)
        {
            value = resV;
            return true;
        }
        value = default;
        return false;
    }

#nullable enable
    public static TVal? GetValueOrDefaultBetter<TKey, TVal>(
        this IReadOnlyDictionary<TKey, TVal>? src,
        TKey? key
    ) where TKey : class
    {
        if (src is null || key is not TKey k)
        {
            return default;
        }
        return src.GetValueOrDefault(k);
    }

    public static TVal? GetValueOrDefaultBetter<TKey, TVal>(
        this IReadOnlyDictionary<TKey, TVal>? src,
        TKey? key
    ) where TKey : struct
    {
        if (src is null || key is not TKey k)
        {
            return default;
        }
        return src.GetValueOrDefault(k);
    }

}
