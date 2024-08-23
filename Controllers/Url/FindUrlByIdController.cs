using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Services.Models;

namespace Shortener.Controllers.Url
{
    [ApiController]
    [Route("/find/{id}")]
    public class FindUrlByIdController(IFindUrlByIdService findUrlByIdService) : ControllerBase
    {
        private readonly IFindUrlByIdService _findUrlByIdService = findUrlByIdService;

        [HttpGet]
        public async Task<IActionResult> Handle(string id)
        {
            if (id == null)
                return BadRequest(new BadRequestHandler() { Message = "Url id is missing" });

            try
            {
                var url = await _findUrlByIdService.Execute(int.Parse(id));

                if (url == null)
                    return NotFound(new NotFoundHandler());

                return Ok(new FindUrlByIdResponse(url));
            }
            catch (FormatException)
            {
                return BadRequest(new BadRequestHandler() { Message = "Invalid url id" });
            }
        }
    }

    public class FindUrlByIdResponse(Models.Url data) : OkHandler<Models.Url>(data)
    {
    }
}