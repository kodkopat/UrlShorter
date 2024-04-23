using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UrlShorter.Application.Dtos;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Domain.Entities;
using UrlShorter.Domain.Interfaces;
using UrlShorter.Domain.Interfaces.Repositories;

namespace UrlShorter.Application.Services
{
    public class UrlService(IUnitOfWork unitOfWork,
                            IUrlRepository urlRepository,
                            IMapper mapper,
                            IHttpContextAccessor httpContext) : IUrlService
    {
        private static readonly Random random = new Random();
        private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IUrlRepository urlRepository = urlRepository;
        private readonly IMapper mapper = mapper;
        private readonly IHttpContextAccessor httpContext = httpContext;
        public async Task<UrlDto> Get(string key)
        {
            var item = await urlRepository.GetByKeyAsync(key);
            return new UrlDto { Url = item.Url, ShortUrl = $"{httpContext.HttpContext.Request.Host}/{item.Key}", Count = item.Count };
        }
        public async Task<UrlDto> GetAndIncreaseCount(string key)
        {
            await urlRepository.IncreaseCount(key);
            return await Get(key);
        }
        public async Task<UrlDto> Create(string url)
        {

            var uniqueKey = await GenerateUniqueKeyAsync();
            var shortUrl = new Urls
            {
                Url = url,
                Key = uniqueKey,
                Count = 0
            };

            await urlRepository.AddAsync(shortUrl);
            await unitOfWork.CompleteAsync();

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
                keyExists = await urlRepository.KeyExistAsync(newKey);
            }
            while (keyExists);
            return newKey;
        }
    }
}