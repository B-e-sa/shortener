using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Services.Url.Models;
using Shortener.Controllers.ResponseHandlers;

namespace Shortener.Web.Controllers.Url
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

    public class GetTopUrlsResponse(List<Domain.Entities.Url> data) : OkHandler<List<Domain.Entities.Url>>(data)
    {
    }
}