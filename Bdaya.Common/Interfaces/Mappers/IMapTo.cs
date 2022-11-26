namespace Bdaya.Mappers;
[Obsolete]
public interface IMapTo<in TTo>
{
    void AssignValuesTo(TTo to);
}
[Obsolete]
public interface IMapToCreate<out TTo>
{
    TTo MapTo();
}
