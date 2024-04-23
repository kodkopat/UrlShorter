using UrlShorter.Application.Dtos;

namespace UrlShorter.Application.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> Create(string url);
       
    }
}