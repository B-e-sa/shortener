using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/find")]
    public class FindUrlByIdController(IFindUrlByIdService findUrlByIdService) : ControllerBase
    {
        private readonly IFindUrlByIdService _findUrlByIdService = findUrlByIdService;

        [HttpPost("{id}")]
        public async Task<IActionResult> Handle(string id)
        {
            if (id == null)
                return BadRequest(new BadRequestHandler() { Message = "Url id is missing" });

            try
            {
                int.Parse(id);
            }
            catch (FormatException)
            {
                return BadRequest(new BadRequestHandler() { Message = "Invalid url id" });
            }

            var url = await _findUrlByIdService.Execute(id);

            if (url == null)
                return NotFound(new NotFoundHandler());

            return Ok(new FindUrlByIdResponse(url));
        }
    }

    public class FindUrlByIdResponse(Url data) : OkHandler<Url>(data)
    {
    }
}