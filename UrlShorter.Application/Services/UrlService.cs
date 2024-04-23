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
                            IHttpContextAccessor httpContext) : IUrlService
    {
        private static readonly Random random = new Random();
        private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUrlRepository _urlRepository = urlRepository;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<UrlDto> Get(string url)
        {
            var key = UrlHelper.ExtractKeyFromUrl(url);
            var item = await _urlRepository.GetByKeyAsync(key);
            return new UrlDto { Url = item.Url, ShortUrl = $"{_httpContext.HttpContext.Request.Host}/{item.Key}", Count = item.Clicks.Count };
        }
        public async Task<UrlDto> GetAndIncreaseCount(string key)
        {
            await _urlRepository.IncreaseCount(key, _httpContext.HttpContext.Request.Headers["User-Agent"].ToString());
            return await Get(key);
        }
        public async Task<UrlDto> Create(string url)
        {
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
        private string GenerateUniqueKey(int length = 6)
        {
            return new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
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