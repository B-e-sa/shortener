using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
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

        var trimmedNewPassword = req.NewPassword.Trim();

        if(_encryptionProvider.Verify(foundUser.Password, trimmedNewPassword))
        {
            throw new InvalidNewPasswordException();
        }

        foundUser.Password = _encryptionProvider.Hash(trimmedNewPassword);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
