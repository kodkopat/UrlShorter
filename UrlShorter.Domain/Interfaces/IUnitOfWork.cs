using UrlShorter.Domain.Interfaces.Repositories;

namespace UrlShorter.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUrlRepository Urls { get; init; }

        Task<int> CompleteAsync();
    }
}