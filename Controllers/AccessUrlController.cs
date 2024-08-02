using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
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
                return BadRequest(new BadRequestHandler()
                {
                    Message = "Invalid URL"
                });

            var foundUrl = await _findByShortUrlService.Execute(url);

            if (foundUrl == null)
                return NotFound(new NotFoundHandler());

            return Redirect(foundUrl.OriginalUrl);
        }
    }
}