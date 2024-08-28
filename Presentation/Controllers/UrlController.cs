using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Commands.Url.CreateUrl;

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
        }
    }
}