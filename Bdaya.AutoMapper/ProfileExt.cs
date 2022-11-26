namespace AutoMapper;
using System.Collections;
using System.Linq.Expressions;

public static class ProfileExt
{
    /// <summary>
    /// Maps members from <typeparamref name="TRes"/> to <typeparamref name="TMember"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    /// <param name="src"></param>
    /// <param name="destinationMember"></param>
    /// <param name="srcMember"></param>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDestination> ForMemberMapFrom<
        TSource,
        TDestination,
        TMember,
        TRes
    >(
        this IMappingExpression<TSource, TDestination> src,
        Expression<Func<TDestination, TMember>> destinationMember,
        Expression<Func<TSource, TRes>> srcMember
    )
    {
        return src.ForMember(destinationMember, o => o.MapFrom(srcMember));
    }

    /// <summary>
    /// Maps members from <typeparamref name="TRes"/> to <typeparamref name="TMember"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    /// <param name="src"></param>
    /// <param name="destinationMember"></param>
    /// <param name="mapFrom"></param>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDestination> ForMemberWithContext<
        TSource,
        TDestination,
        TMember,
        TRes
    >(
        this IMappingExpression<TSource, TDestination> src,
        Expression<Func<TDestination, TMember>> destinationMember,
        Func<TSource, ResolutionContext, TRes> mapFrom
    )
    {
        return src.ForMember(
            destinationMember,
            o =>
                o.MapFrom(
                    (src, dst, member, context) =>
                    {
                        return mapFrom(src, context);
                    }
                )
        );
    }

    /// <summary>
    /// Maps <typeparamref name="TRes"/> to <typeparamref name="TMember"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    /// <typeparam name="TEntryType"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    /// <param name="src"></param>
    /// <param name="destinationMember"></param>
    /// <param name="entryName"></param>
    /// <param name="mapFrom"></param>
    /// <param name="defaultIfNotPresent"></param>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDestination> ForMemberWithItemEntry<
        TSource,
        TDestination,
        TMember,
        TEntryType,
        TRes
    >(
        this IMappingExpression<TSource, TDestination> src,
        Expression<Func<TDestination, TMember>> destinationMember,
        string entryName,
        Func<TSource, TEntryType, TRes> mapFrom,
        Func<TEntryType>? defaultIfNotPresent = null
    )
    {
        return ForMemberWithItemEntry(
            src,
            destinationMember,
            entryName,
            (TSource src, ResolutionContext context, TEntryType entry) =>
            {
                return mapFrom(src, entry);
            },
            defaultIfNotPresent
        );
    }

    /// <summary>
    /// Maps members from <typeparamref name="TRes"/> to <typeparamref name="TMember"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    /// <typeparam name="TEntryType"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    /// <param name="src"></param>
    /// <param name="destinationMember"></param>
    /// <param name="entryName"></param>
    /// <param name="mapFrom"></param>
    /// <param name="defaultIfNotPresent"></param>
    /// <returns></returns>
    /// <exception cref="AutoMapperMappingException"></exception>
    public static IMappingExpression<TSource, TDestination> ForMemberWithItemEntry<
        TSource,
        TDestination,
        TMember,
        TEntryType,
        TRes
    >(
        this IMappingExpression<TSource, TDestination> src,
        Expression<Func<TDestination, TMember>> destinationMember,
        string entryName,
        Func<TSource, ResolutionContext, TEntryType, TRes> mapFrom,
        Func<TEntryType>? defaultIfNotPresent = null
    )
    {
        return src.ForMember(
            destinationMember,
            o =>
                o.MapFrom(
                    (src, dst, member, context) =>
                    {
                        if (!context.Items.TryGetValueCasted(entryName, out TEntryType? value))
                        {
                            if (defaultIfNotPresent != null)
                            {
                                value ??= defaultIfNotPresent();
                            }
                            else
                            {
                                throw new AutoMapperMappingException(
                                    $"Missing '{entryName}' of type {typeof(TEntryType)} in {nameof(context.Items)}"
                                );
                            }
                        }
                        return mapFrom(src, context, value);
                    }
                )
        );
    }
}
