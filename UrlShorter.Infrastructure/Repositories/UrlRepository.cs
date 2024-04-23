using UrlShorter.Domain.Entities;
using UrlShorter.Domain.Interfaces.Repositories;
using UrlShorter.Infrastructure.Persistence;

namespace UrlShorter.Infrastructure.Repositories
{
    public class UrlRepository(AppDbContext context) : IUrlRepository
    {
        public async Task<IEnumerable<Urls?>> GetAllAsync()
        {
            return await context.Urls.ToListAsync();
        }
        public async Task<Urls?> GetByKeyAsync(string key)
        {
            return await context.Urls.FirstOrDefaultAsync(c => c.Key.Equals(key));
        }
        public async Task AddAsync(Urls? url)
        {
            await context.Urls.AddAsync(url);
        }
        public Task<bool> KeyExistAsync(string key)
        {
            return context.Urls.AnyAsync(c => c.Key.Equals(key));
        }
        public async Task IncreaseCount(string key)
        {
            var item = await GetByKeyAsync(key);
            item.Count++;
            await context.SaveChangesAsync();
        }
    }
}