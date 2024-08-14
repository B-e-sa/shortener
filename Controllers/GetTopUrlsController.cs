using Microsoft.AspNetCore.Mvc;
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
            return Ok(urls);
        }
    }
}