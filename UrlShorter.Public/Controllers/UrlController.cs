using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Infrastructure.Helpers;

namespace UrlShorter.Public.Controllers
{
    public class UrlController(IUrlService urlService, IHttpContextAccessor contextAccessor) : Controller
    {
        private readonly IUrlService _urlService = urlService;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
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

        [HttpGet, ActionName("Statistics")]
        public async Task<IActionResult> Statistics_get(string key)
        {
           
            return View();
        }
        [HttpPost, ActionName("Statistics")]
        public async Task<IActionResult> Statistics_post(string url)
        {
            var statistics = await _urlService.GetStatisticsByDayAsync(url);
            return View(statistics);
        }
    }
}
