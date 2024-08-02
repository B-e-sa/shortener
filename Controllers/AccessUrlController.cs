using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class AccessUrlController(
        IFindByShortUrlService findByShortUrlService,
        IVisitUrlService visitUrlService
    ) : ControllerBase
    {
        private readonly IFindByShortUrlService _findByShortUrlService = findByShortUrlService;
        private readonly IVisitUrlService _visitUrlService = visitUrlService;

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

            await _visitUrlService.Execute(foundUrl);
            return Redirect(foundUrl.OriginalUrl);
        }
    }
}