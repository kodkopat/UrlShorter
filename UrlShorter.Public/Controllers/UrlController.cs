using Microsoft.AspNetCore.Mvc;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Infrastructure.Helpers;

namespace UrlShorter.Public.Controllers
{
    public class UrlController(IUrlService urlService) : Controller
    {
        private readonly IUrlService _urlService = urlService;

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Short(string url)
        {
            if (UrlHelper.IsValidUrl(url))
            {
                var result = await _urlService.Create(url);
                return View(result);
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> RedirectUrl(string key)
        {
            var result = await _urlService.GetAndIncreaseCount(key);
            return Redirect(result.Url);
        }
        [HttpGet,Route("url/count/{key}")]
        public async Task<IActionResult> Count(string key)
        {
            var result = await _urlService.Get(key);
            return View(result);
        }
    }
}
