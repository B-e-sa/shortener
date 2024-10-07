using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.VerifyEmail;

public record VerifyEmailCommand(string Code, string Token) : IRequest;

class VerifyEmailCommandHandler(
    IAppDbContext context,
    ITokenService tokenService) : IRequestHandler<VerifyEmailCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task Handle(VerifyEmailCommand req, CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        if (foundUser.ConfirmedEmail)
        {
            throw new UserEmailAlreadyVerifiedException();
        }

        var foundVerification = await _context.EmailVerifications
            .Where(e => e.Code == req.Code.Trim())
            .FirstOrDefaultAsync(cancellationToken)
             ?? throw new EmailVerificationNotFoundException();

        if (foundUser.Id != foundVerification.UserId)
        {
            throw new UnauthorizedAccessException("Only the account owner can verify it.");
        }

        foundUser.ConfirmedEmail = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}