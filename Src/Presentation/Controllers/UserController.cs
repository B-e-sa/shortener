using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Users.Commands.AuthenticateToken;
using Shortener.Application.Users.Commands.CreateUser;
using Shortener.Application.Users.Commands.DeleteUser;
using Shortener.Application.Users.Commands.Login;
using Shortener.Application.Users.Queries.FindUserById;
using Shortener.Presentation.Common;

namespace Shortener.Presentation.Controllers;

public class UserController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand req,
        CancellationToken cancellationToken
    )
    {
        var command = req.Adapt<CreateUserCommand>();

        var userId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(CreateUser), new { userId }, userId);
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
}