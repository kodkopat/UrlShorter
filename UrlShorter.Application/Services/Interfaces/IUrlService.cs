using UrlShorter.Application.Dtos;

namespace UrlShorter.Application.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> AddProductAsync(UrlDto productDto);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<UrlDto>> GetAllProductsAsync();
        Task<UrlDto> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync(UrlDto productDto);
    }
}