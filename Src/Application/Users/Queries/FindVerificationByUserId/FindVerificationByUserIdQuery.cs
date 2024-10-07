using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Queries.FindVerificationByUserId;

public sealed record FindVerificationByUserIdQuery(string Token) : IRequest<EmailVerification>;

internal sealed class FindVerificationByUserIdQueryHandler(
    IAppDbContext context,
    ITokenService tokenService)
    : IRequestHandler<FindVerificationByUserIdQuery, EmailVerification>
{
    readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<EmailVerification> Handle(
        FindVerificationByUserIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        var foundVerification = await _context
            .EmailVerifications
            .Where(v => v.UserId == foundUser.Id)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EmailVerificationNotFoundException();

        if (foundUser.Id != foundVerification.UserId)
        {
            throw new UnauthorizedAccessException("Only account owner can search its verification.");
        }

        return foundVerification;
    }
}