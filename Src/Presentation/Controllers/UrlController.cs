using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Application.Urls.Commands.DeleteUrl;
using Shortener.Application.Urls.Queries.FindUrlById;
using Shortener.Application.Urls.Queries.FindUrlByShortUrl;
using Shortener.Application.Urls.Queries.GetTopUrls;

namespace Shortener.Presentation.Controllers;

public class UrlController : ApiController
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

    [HttpGet]
    public async Task<IActionResult> GetTop(
        CancellationToken cancellationToken
    )
    {
        var query = new GetTopUrlsQuery();

        var foundUrls = await Sender.Send(query, cancellationToken);

        return Ok(foundUrls);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(
        int id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteUrlCommand(id);

        await Sender.Send(command, cancellationToken);

        return Ok(id);
    }
}