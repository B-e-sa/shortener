using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers.Url
{
    [ApiController]
    [Route("/")]
    public class DeleteUrlController(IDeleteUrlService deleteUrlService, IFindUrlByIdService findUrlByIdService) : ControllerBase
    {
        private readonly IDeleteUrlService _deleteUrlService = deleteUrlService;
        private readonly IFindUrlByIdService _findUrlByIdService = findUrlByIdService;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Handle(string id)
        {
            if (id == null)
                return BadRequest(new BadRequestHandler() { Message = "Url id is missing" });

            try
            {
                var url = await _findUrlByIdService.Execute(int.Parse(id));

                if (url == null)
                    return NotFound(new NotFoundHandler());

                await _deleteUrlService.Execute(url);
                return Ok(new DeleteUrlResponse(url));
            }
            catch (FormatException)
            {
                return BadRequest(new BadRequestHandler() { Message = "Invalid url id" });
            }
        }
    }

    public class DeleteUrlResponse(Models.Url data) : OkHandler<Models.Url>(data)
    {
    }
}