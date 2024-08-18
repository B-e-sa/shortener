using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class CreateUrlController(ICreateUrlService createUrlService) : ControllerBase
    {
        private readonly ICreateUrlService _createUrlService = createUrlService;

        [HttpPost()]
        public async Task<IActionResult> Handle([FromBody] UrlCreateRequest req)
        {
            if (
                req == null
                || string.IsNullOrEmpty(req.Title)
                || string.IsNullOrEmpty(req.Url)
            )
                return BadRequest(new BadRequestHandler()
                {
                    Message = "The object must contain the Url and the respective title"
                });

            var urlReg = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)";

            if (!Regex.Match(req.Url, urlReg).Success)
                return BadRequest(new BadRequestHandler()
                {
                    Message = "Invalid Url format"
                });

            var createdUrl = await _createUrlService.Execute(req.Url, req.Title);
            return Created(nameof(req), new CreatedHandler<Url>(createdUrl));
        }
    }

    public class UrlCreateRequest(string? title, string? url)
    {
        public string? Url { get; set; } = url;
        public string? Title { get; set; } = title;
    }
}