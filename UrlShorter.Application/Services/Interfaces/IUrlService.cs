using UrlShorter.Application.Dtos;

namespace UrlShorter.Application.Services.Interfaces
{
    public interface IUrlService
    {
        Task<GetUrlDto> Create(string url);
        Task<GetUrlDto> Get(string url);
        Task<GetUrlDto> GetAndIncreaseCount(string key);
        Task<IEnumerable<DailyStatisticsDto>> GetStatisticsByDayAsync(string key);
    }
}