using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Users.Commands.CreateUser;
using Shortener.Application.Users.Commands.DeleteUser;
using Shortener.Application.Users.Commands.Login;
using Shortener.Application.Users.Queries.FindUserById;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(
        int id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteUserCommand(id);

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

    [HttpPost("auth")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand req,
        CancellationToken cancellationToken
    )
    {
        var command = req.Adapt<LoginCommand>();

        var token = await Sender.Send(command, cancellationToken);

        return Ok(token);
    }
}