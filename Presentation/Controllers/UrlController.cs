using System.Text.RegularExpressions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Commands.Url;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;

namespace Shortener.Presentation.Controllers
{
    public class UrlController() : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Handle(
            [FromBody] CreateUrlCommand req,
            CancellationToken cancellationToken
        )
        {
            var command = req.Adapt<CreateUrlCommand>();

            var urlId = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Handle), new { urlId }, urlId);
            
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
        }
    }
}