using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.NewPasswordRequests;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.CreateNewPassword;

public record CreateNewPasswordCommand(string Code, string NewPassword) : IRequest;

public class CreateNewPasswordCommandHandler(
    IAppDbContext dbContext,
    IEncryptionProvider encryptionProvider) : IRequestHandler<CreateNewPasswordCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IEncryptionProvider _encryptionProvider = encryptionProvider;

    public async Task Handle(CreateNewPasswordCommand req, CancellationToken cancellationToken)
    {
        var foundRequest = await _dbContext
            .NewPasswordRequests
            .Where(p => p.Code == req.Code)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NewPasswordRequestNotFoundException();

        var foundUser = await _dbContext
            .Users
            .FindAsync([foundRequest.UserId], cancellationToken)
            ?? throw new UserNotFoundException();

        if(_encryptionProvider.Verify(foundUser.Password, req.NewPassword))
        {
            throw new InvalidNewPasswordException();
        }

        foundUser.Password = _encryptionProvider.Hash(req.NewPassword);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
