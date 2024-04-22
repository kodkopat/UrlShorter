using AutoMapper;
using UrlShorter.Application.Dtos;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Domain.Entities;
using UrlShorter.Domain.Interfaces;

namespace UrlShorter.Application.Services
{
    public class UrlService(IUnitOfWork unitOfWork, IMapper mapper) : IUrlService
    {
        public async Task<IEnumerable<UrlDto>> GetAllProductsAsync()
        {
            var url = await unitOfWork.Urls.GetAllAsync();
            return mapper.Map<IEnumerable<UrlDto>>(url);
        }
        public async Task<UrlDto> GetProductByIdAsync(Guid id)
        {
            var url = await unitOfWork.Urls.GetByIdAsync(id);
            return mapper.Map<UrlDto>(url);
        }
        public async Task<UrlDto> AddProductAsync(UrlDto urlDto)
        {
            var url = mapper.Map<Urls>(urlDto);
            await unitOfWork.Urls.AddAsync(url);
            await unitOfWork.CompleteAsync();
            urlDto.Id = url.Id;
            return urlDto;
        }
        public async Task UpdateProductAsync(UrlDto urlDto)
        {
            var existingUrl = await unitOfWork.Urls.GetByIdAsync(urlDto.Id);
            if (existingUrl == null) throw new KeyNotFoundException("Url not found.");

            mapper.Map(urlDto, existingUrl);
            await unitOfWork.Urls.UpdateAsync(existingUrl);
            await unitOfWork.CompleteAsync();
        }
        public async Task DeleteProductAsync(Guid id)
        {
            await unitOfWork.Urls.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
        }
    }
}