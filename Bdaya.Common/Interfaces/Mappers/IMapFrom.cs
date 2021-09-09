using System.Linq.Expressions;

namespace Bdaya.Mappers;

public interface IMapFrom<TFrom>
{
    void AssignValuesFrom(TFrom from);
}
public interface IMapFromCreate<TSelf, TFrom>
{
    static abstract TSelf MapFrom(TFrom from);
}
public interface IMapFromCreateExpression<TSelf, TFrom>
{
    static abstract Expression<Func<TFrom, TSelf>> MapFrom();
}
public interface IMapFromCreate1<TSelf, TFrom, TItem1>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict);
}
public interface IMapFromCreate2<TSelf, TFrom, TItem1, TItem2>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict, IReadOnlyDictionary<string, TItem2> items2Dict);
}
public interface IMapFromCreate3<TSelf, TFrom, TItem1, TItem2, TItem3>
{
    static abstract TSelf MapFrom(TFrom from, IReadOnlyDictionary<string, TItem1> items1Dict, IReadOnlyDictionary<string, TItem2> items2Dict, IReadOnlyDictionary<string, TItem3> items3Dict);
}
