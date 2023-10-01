namespace Core.Config
{
    public interface IBaseSingleConfig<T> where T : class, IConfigData
    {
        T Data { get; }
    }
}