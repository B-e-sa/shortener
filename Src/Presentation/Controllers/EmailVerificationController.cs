using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.EmailVerifications.Commands;
using Shortener.Application.EmailVerifications.Queries.FindVerificationByUserId;
using Shortener.Presentation.Common;

namespace Shortener.Presentation.Controllers;

[Route("verification")]
public class EmailVerificationController : ApiController
{
    [HttpPost("verify")]
    public async Task<IActionResult> Verify(
        [FromBody] VerifyEmailCommand req,
        CancellationToken cancellationToken
    )
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        req = req with { Token = token };

        var command = req.Adapt<VerifyEmailCommand>();

        await Sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Find(
        [FromBody] FindVerificationByUserIdQuery req,
        CancellationToken cancellationToken
    )
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        req = req with { Token = token };

        var command = req.Adapt<FindVerificationByUserIdQuery>();

        await Sender.Send(command, cancellationToken);

        return Ok();
    }
}