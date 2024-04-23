using UrlShorter.Domain.Entities;

namespace UrlShorter.Domain.Interfaces.Repositories
{
    public interface IUrlRepository
    {
        Task<IEnumerable<Urls?>> GetAllAsync();
        Task<Urls?> GetByKeyAsync(string key);
        Task AddAsync(Urls? url);
        Task<bool> KeyExistAsync(string key);
        Task IncreaseCount(string key, string? UserAgentString);
    }
}