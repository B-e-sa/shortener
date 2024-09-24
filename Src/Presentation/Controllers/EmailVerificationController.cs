using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.EmailVerifications.Commands.CreateEmailVerification;
using Shortener.Application.EmailVerifications.Commands.VerifyEmail;
using Shortener.Application.EmailVerifications.Queries.FindVerificationByUserId;
using Shortener.Presentation.Common;

namespace Shortener.Presentation.Controllers;

public class VerificationController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = new { Token=token }.Adapt<CreateEmailVerificationCommand>();

        var emailVerificationId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { emailVerificationId }, emailVerificationId);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify(
        [FromBody] VerifyEmailCommand req,
        CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = (req with { Token = token }).Adapt<VerifyEmailCommand>();

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

        var command = new { Token = token }.Adapt<FindVerificationByUserIdQuery>();

        await Sender.Send(command, cancellationToken);

        return Ok();
    }
}