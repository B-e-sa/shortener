using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.EmailVerifications.Commands.CreateEmailVerification;

public record CreateEmailVerificationCommand(string Token) : IRequest<EmailVerification>;

public class CreateEmailVerificationCommandHandler(
    IAppDbContext context,
    IJwtProvider jwtProvider) : IRequestHandler<CreateEmailVerificationCommand, EmailVerification>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<EmailVerification> Handle(
        CreateEmailVerificationCommand req,
        CancellationToken cancellationToken)
    {
        var payload = _jwtProvider.Read(req.Token);

        var foundUser = await _context
            .Users
            .FindAsync([payload.Id], cancellationToken)
            ?? throw new UserNotFoundException();

        var olderVerifications = _context
            .EmailVerifications
            .Where(e => e.UserId == payload.Id);

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