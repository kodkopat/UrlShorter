using UrlShorter.Domain.Entities;

namespace UrlShorter.Domain.Interfaces.Repositories
{
    public interface IUrlRepository
    {
        Task<IEnumerable<Urls?>> GetAllAsync();
        Task<Urls?> GetByIdAsync(Guid id);
        Task AddAsync(Urls? product);
        Task UpdateAsync(Urls? product);
        Task DeleteAsync(Guid id);
    }
}