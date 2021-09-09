namespace Bdaya.Mappers;

public interface IMapTo<TTo>
{
    void AssignValuesTo(TTo to);
}
public interface IMapToCreate<TTo>
{
    TTo MapTo();
}
