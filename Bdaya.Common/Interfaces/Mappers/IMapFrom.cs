using System.Linq.Expressions;

namespace Bdaya.Mappers;

public interface IMapFrom<in TFrom>
{
    void AssignValuesFrom(TFrom from);
}

#if NET6_0_OR_GREATER

public interface IMapFromCreate<out TSelf, in TFrom>
{
    static abstract TSelf MapFrom(TFrom from);
}
public interface IMapFromCreateExpression<TSelf, TFrom>
{
    static abstract Expression<Func<TFrom, TSelf>> MapFrom();
}
public interface IMapFromCreate1<out TSelf, in TFrom, TItem1>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict);
}
public interface IMapFromCreate2<out TSelf, in TFrom, TItem1, TItem2>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict, IReadOnlyDictionary<string, TItem2> items2Dict);
}
public interface IMapFromCreate3<out TSelf, in TFrom, TItem1, TItem2, TItem3>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict, IReadOnlyDictionary<string, TItem2> items2Dict, IReadOnlyDictionary<string, TItem3> items3Dict);
}

#else
#endif