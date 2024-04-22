using UrlShorter.Domain.Interfaces;
using UrlShorter.Domain.Interfaces.Repositories;
using UrlShorter.Infrastructure.Persistence;

namespace UrlShorter.Infrastructure
{
    public class UnitOfWork(AppDbContext context,
                            IUrlRepository urlRepository)
        : IUnitOfWork, IDisposable
    {
        public IUrlRepository Urls { get; init; } = urlRepository;
        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}