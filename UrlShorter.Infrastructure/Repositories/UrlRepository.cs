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
        public async Task<Urls?> GetByIdAsync(Guid id)
        {
            return await context.Urls.FindAsync(id);
        }
        public async Task AddAsync(Urls? url)
        {
            await context.Urls.AddAsync(url);
        }
        public async Task UpdateAsync(Urls? url)
        {
            context.Urls.Update(url);
        }
        public async Task DeleteAsync(Guid id)
        {
            var url = await context.Urls.FindAsync(id);
            if (url != null)
            {
                context.Urls.Remove(url);
                await context.SaveChangesAsync();
            }
        }
    }
}