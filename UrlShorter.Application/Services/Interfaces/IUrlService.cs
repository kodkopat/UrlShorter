using UrlShorter.Application.Dtos;

namespace UrlShorter.Application.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> Create(string url);
        Task<UrlDto> Get(string url);
        Task<UrlDto> GetAndIncreaseCount(string key);
    }
}