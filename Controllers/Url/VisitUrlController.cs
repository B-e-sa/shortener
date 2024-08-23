using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers.Url
{
    [ApiController]
    [Route("/")]
    public class VisitUrlController(
        IFindUrlByShortUrlService findUrlByShortUrlService,
        IVisitUrlService visitUrlService
    ) : ControllerBase
    {
        private readonly IFindUrlByShortUrlService _findUrlByShortUrlService = findUrlByShortUrlService;
        private readonly IVisitUrlService _visitUrlService = visitUrlService;

        [HttpGet("{url}")]
        public async Task<IActionResult> Handle(string url)
        {
            if (url.Length > 4 || url.Length < 4)
                return BadRequest(new BadRequestHandler()
                {
                    Message = "Invalid Url"
                });

            var foundUrl = await _findUrlByShortUrlService.Execute(url);

            if (foundUrl == null)
                return NotFound(new NotFoundHandler());

            await _visitUrlService.Execute(foundUrl);
            return Redirect(foundUrl.OriginalUrl);
        }
    }
}