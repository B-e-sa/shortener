using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Models;
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
            return Ok(new GetTopUrlsResponse(urls));
        }
    }

    public class GetTopUrlsResponse(List<Url> data) : OkHandler<List<Url>>(data)
    {
    }
}