using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.CreateEmailVerification;

public record CreateEmailVerificationCommand(string Token) : IRequest<EmailVerification>;

public class CreateEmailVerificationCommandHandler(
    IAppDbContext context,
    ITokenService tokenService) : IRequestHandler<CreateEmailVerificationCommand, EmailVerification>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<EmailVerification> Handle(
        CreateEmailVerificationCommand req,
        CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        if (foundUser.ConfirmedEmail)
        {
            throw new UserEmailAlreadyVerifiedException();
        }

        var olderVerifications = _context
            .EmailVerifications
            .Where(e => e.UserId == foundUser.Id);

        _context.EmailVerifications.RemoveRange(olderVerifications);

        EmailVerification newEmailVerification = new()
        {
            User = foundUser,
        };

        _context.EmailVerifications.Add(newEmailVerification);
        await _context.SaveChangesAsync(cancellationToken);

        return newEmailVerification;
    }
}