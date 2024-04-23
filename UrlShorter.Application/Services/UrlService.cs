using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using UrlShorter.Application.Dtos;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Domain.Entities;
using UrlShorter.Domain.Interfaces;
using UrlShorter.Domain.Interfaces.Repositories;
using UrlShorter.Infrastructure.Helpers;

namespace UrlShorter.Application.Services
{
    public class UrlService(IUnitOfWork unitOfWork,
                            IUrlRepository urlRepository,
                            IHttpContextAccessor httpContextAccessor) : IUrlService
    {
        private static readonly Random Random = new Random();
        private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IUrlRepository _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<GetUrlDto> Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("URL cannot be null or empty.", nameof(key));

        
            var item = await _urlRepository.GetByKeyAsync(key);

            if (item == null)
                throw new KeyNotFoundException($"URL with key '{key}' not found.");

            var host = _httpContextAccessor.HttpContext?.Request?.Host;
            var shortUrl = host != null ? $"{host}/{item.Key}" : item.Key;

            return new GetUrlDto { Url = item.Url, ShortUrl = shortUrl, Count = item.Clicks.Count };
        }
        public async Task<GetUrlDto> GetAndIncreaseCount(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";
            await _urlRepository.IncreaseCount(key, userAgent);

            return await Get(key);
        }
        public async Task<GetUrlDto> Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));

            var uniqueKey = await GenerateUniqueKeyAsync();

            var shortUrl = new Urls
            {
                Url = url,
                Key = uniqueKey,
            };

            await _urlRepository.AddAsync(shortUrl);
            await _unitOfWork.CompleteAsync();

            return await Get(uniqueKey);
        }
        public async Task<IEnumerable<DailyStatisticsDto>> GetStatisticsByDayAsync(string url)
        {
            var key = UrlHelper.ExtractKeyFromUrl(url);
            var item = await _urlRepository.GetByKeyAsync(key);

            if (item == null)
                throw new KeyNotFoundException("URL not found.");

            var statisticsByDay = item.Clicks
                .GroupBy(click => click.CreateAt.Date)
                .Select(group => new DailyStatisticsDto
                {
                    Date = group.Key,
                    ClickCount = group.Count(),
                });

            return statisticsByDay;
        }

        private string GenerateUniqueKey(int length = 6)
        {
            return new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        private async Task<string> GenerateUniqueKeyAsync(int length = 6)
        {
            string newKey;
            bool keyExists;

            do
            {
                newKey = GenerateUniqueKey(length);
                keyExists = await _urlRepository.KeyExistAsync(newKey);
            }
            while (keyExists);

            return newKey;
        }
    }
}
