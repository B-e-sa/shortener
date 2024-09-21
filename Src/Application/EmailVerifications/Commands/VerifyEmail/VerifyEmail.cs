using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.EmailVerifications;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.EmailVerifications.Commands.VerifyEmail;

public record VerifyEmailCommand(string Code, string Token) : IRequest;

class VerifyEmailCommandHandler(
    IAppDbContext context,
    IJwtProvider jwtProvider) : IRequestHandler<VerifyEmailCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task Handle(VerifyEmailCommand req, CancellationToken cancellationToken)
    {
        var foundVerification = await _context.EmailVerifications
            .Where(e => e.Code == req.Code)
            .FirstOrDefaultAsync(cancellationToken)
             ?? throw new EmailVerificationNotFoundException();

        var payload = _jwtProvider.Read(req.Token);

        if (payload.Id != foundVerification.UserId)
        {
            throw new UnauthorizedAccessException("Only the account owner can verify it.");
        }

        var foundUser = await _context
            .Users
            .FindAsync([payload.Id], cancellationToken)
            ?? throw new UserNotFoundException();

        foundUser.ConfirmedEmail = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}