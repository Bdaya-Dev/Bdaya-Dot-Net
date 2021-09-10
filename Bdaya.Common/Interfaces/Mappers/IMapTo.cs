namespace Bdaya.Mappers;

public interface IMapTo<in TTo>
{
    void AssignValuesTo(TTo to);
}
public interface IMapToCreate<out TTo>
{
    TTo MapTo();
}
