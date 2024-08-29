using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Url.Commands.CreateUrl;
using Shortener.Application.Url.Queries;
using Shortener.Application.Url.Queries.FindUrlByShortUrl;

namespace Shortener.Presentation.Controllers
{
    public class UrlController() : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUrl(
            [FromBody] CreateUrlCommand req,
            CancellationToken cancellationToken
        )
        {
            var command = req.Adapt<CreateUrlCommand>();

            var urlId = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(CreateUrl), new { urlId }, urlId);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> FindById(
            int id,
            CancellationToken cancellationToken
        )
        {
            var query = new FindUrlByIdQuery(id);

            var foundUrl = await Sender.Send(query, cancellationToken);

            return Ok(foundUrl);
        }

        [HttpGet("{url}")]
        public async Task<IActionResult> FindByShortUrl(
            string url,
            CancellationToken cancellationToken
        )
        {
            var query = new FindUrlByShortUrlQuery(url);

            var foundUrl = await Sender.Send(query, cancellationToken);

            return Ok(foundUrl);
        }
    }
}