using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Application.Urls.Commands.DeleteUrl;
using Shortener.Application.Urls.Queries.FindUrlByShortUrl;
using Shortener.Application.Urls.Queries.GetTopUrls;
using Shortener.Application.Urls.Queries.GetUrlsByUserId;
using Shortener.Presentation.Common;

namespace Shortener.Presentation.Controllers;

public class UrlController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateUrl(
        [FromBody] CreateUrlCommand req,
        CancellationToken cancellationToken
    )
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        if (!string.IsNullOrEmpty(token))
        {
            req = req with { Token = token };
        }

        var command = req.Adapt<CreateUrlCommand>();

        var urlId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(CreateUrl), new { urlId }, urlId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(
        int id,
        CancellationToken cancellationToken
    )
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = (new { Id = id, Token = token })
            .Adapt<DeleteUrlCommand>();

        await Sender.Send(command, cancellationToken);

        return Ok(id);
    }

    [HttpGet("my-urls")]
    public async Task<IActionResult> GetUserUrlsById(CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var query = (new { Token = token })
            .Adapt<GetUrlsByUserIdQueryHandler>();

        var foundUrls = await Sender.Send(query, cancellationToken);

        return Ok(foundUrls);
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
}