using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Users.Commands.AuthenticateToken;
using Shortener.Application.Users.Commands.CreateEmailVerification;
using Shortener.Application.Users.Commands.CreateNewPassword;
using Shortener.Application.Users.Commands.CreateNewPasswordRequest;
using Shortener.Application.Users.Commands.DeleteUser;
using Shortener.Application.Users.Commands.Login;
using Shortener.Application.Users.Commands.Register;
using Shortener.Application.Users.Commands.VerifyEmail;
using Shortener.Application.Users.Queries.FindUserById;
using Shortener.Application.Users.Queries.FindVerificationByUserId;
using Shortener.Presentation.Common;

namespace Shortener.Presentation.Controllers;

public class UserController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand req,
        CancellationToken cancellationToken
    )
    {
        var command = req.Adapt<RegisterCommand>();

        var userId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Register), new { userId }, userId);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(
        int id,
        CancellationToken cancellationToken
    )
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = new { Token = token }.Adapt<DeleteUserCommand>();

        await Sender.Send(command, cancellationToken);

        return Ok(id);
    }

    [HttpGet("find/{id}")]
    public async Task<IActionResult> FindById(
        int id,
        CancellationToken cancellationToken
    )
    {
        var query = new FindUserByIdQuery(id);

        var foundUser = await Sender.Send(query, cancellationToken);

        return Ok(foundUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand req,
        CancellationToken cancellationToken
    )
    {
        var command = req.Adapt<LoginCommand>();

        var token = await Sender.Send(command, cancellationToken);

        return Ok(token);
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Login(CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = new { Token = token }.Adapt<AuthenticateTokenCommand>();

        var user = await Sender.Send(command, cancellationToken);

        return Ok(user);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> CreateNewPasswordRequest(
        [FromBody] CreateNewPasswordRequestCommand req,
        CancellationToken cancellationToken)
    {
        var command = req.Adapt<CreateNewPasswordRequestCommand>();

        await Sender.Send(command, cancellationToken);

        return Created();
    }

    [HttpPost("new-password")]
    public async Task<IActionResult> CreateNewPassword(
        [FromBody] CreateNewPasswordCommand req,
        CancellationToken cancellationToken)
    {
        var command = req.Adapt<CreateNewPasswordCommand>();

        await Sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("verification")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = new { Token = token }.Adapt<CreateEmailVerificationCommand>();

        var emailVerificationId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { emailVerificationId }, emailVerificationId);
    }

    [HttpGet("verification")]
    public async Task<IActionResult> Find(CancellationToken cancellationToken)
    {
        var token = GetBearerToken.FromHeader(HttpContext);

        var command = new { Token = token }.Adapt<FindVerificationByUserIdQuery>();

        await Sender.Send(command, cancellationToken);

        return Ok();
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
}