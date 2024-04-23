using Microsoft.AspNetCore.Mvc;
using UrlShorter.Application.Services.Interfaces;

namespace UrlShorter.Public.Controllers
{
    public class UrlController(IUrlService urlService) : Controller
    {
        private readonly IUrlService _urlService = urlService;

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Short(string url)
        {
            var result = await _urlService.Create(url);
            return View(result);
        }
    }
}
