using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Shortener.Services;

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
            if (string.IsNullOrEmpty(req.Title) || string.IsNullOrEmpty(req.Url))
                return BadRequest(new { message = "The object must contain the URL and the respective title" });

            var urlReg = @"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

            if (Regex.Match(req.Url, urlReg).Success)
            {
                var createdUrl = await _createUrlService.Execute(req.Url, req.Title);
                return Created(nameof(req), createdUrl);
            }

            return BadRequest(new { message = "Invalid URL format" });
        }
    }

    public class UrlCreateRequest
    {
        public string Url { get; set; }
        public string Title { get; set; }
    }
}