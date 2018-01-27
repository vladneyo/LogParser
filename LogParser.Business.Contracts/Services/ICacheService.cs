namespace LogParser.Business.Contracts.Services
{
    public interface ICacheService<T> where T : class
    {
        T this[string key] { get; set; }
    }
}
