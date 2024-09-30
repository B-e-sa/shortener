using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.CreateNewPasswordRequest;

public record CreateNewPasswordRequestCommand(string Email) : IRequest;

public class CreateNewPasswordRequestCommandHandler(IAppDbContext dbContext) 
    : IRequestHandler<CreateNewPasswordRequestCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Handle(
        CreateNewPasswordRequestCommand req, 
        CancellationToken cancellationToken)
    {
        var foundUser = await _dbContext.Users
            .Where(u => u.Email == req.Email)
            .FirstOrDefaultAsync(cancellationToken)
             ?? throw new UserNotFoundException();

        NewPasswordRequest request = new()
        {
            User = foundUser,
        };

        _dbContext
             .NewPasswordRequests
             .Add(request);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
