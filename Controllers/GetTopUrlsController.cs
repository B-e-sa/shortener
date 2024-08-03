using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class GetTopUrlsController(IGetTopUrlsService getTopUrlsService) : ControllerBase
    {
        private readonly IGetTopUrlsService _getTopUrlsService = getTopUrlsService;

        [HttpGet()]
        public async Task<IActionResult> Handle()
        {
            var urls = await _getTopUrlsService.Execute();

            if (urls.Count == 0)
                return NotFound(new NotFoundHandler()
                {
                    Message = "No url was found"
                });

            return Ok(urls);
        }
    }
}