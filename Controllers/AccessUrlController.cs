using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Shortener.Services;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class AccessUrlController(IFindByShortUrlService findByShortUrlService) : ControllerBase
    {
        private readonly IFindByShortUrlService _findByShortUrlService = findByShortUrlService;

        [HttpGet("{url}")]
        public async Task<IActionResult> Handle(string url)
        {
            if (url.Length > 4 || url.Length < 4)
                return BadRequest(new { message = "Invalid URL" });

            var foundUrl = await _findByShortUrlService.Execute(url);

            if (foundUrl == null)
                return BadRequest(new { message = "This URL does not exist" });

            return Redirect(foundUrl.OriginalUrl);
        }
    }
}