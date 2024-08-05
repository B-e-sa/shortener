using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class DeleteUrlController(IDeleteUrlService deleteUrlService, IFindUrlByIdService findUrlByIdService) : ControllerBase
    {
        private readonly IDeleteUrlService _deleteUrlService = deleteUrlService;
        private readonly IFindUrlByIdService _findUrlByIdService = findUrlByIdService;

        [HttpDelete()]
        public async Task<IActionResult> Handle([FromBody] UrlDeleteRequest req)
        {
            if (req == null || req.Id == null)
                return BadRequest(new BadRequestHandler() { Message = "Url id is missing" });

            var url = await _findUrlByIdService.Execute(req.Id);

            if (url == null)
                return NotFound(new NotFoundHandler());

            await _deleteUrlService.Execute(url);

            return Ok(url);
        }
    }

    public class UrlDeleteRequest
    {
        public string? Id { get; set; }
    }
}