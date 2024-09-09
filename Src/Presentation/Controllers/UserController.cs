using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Users.Commands.CreateUser;

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
}