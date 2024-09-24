﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.EmailVerifications;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.EmailVerifications.Queries.FindVerificationByUserId;

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
        var payload = _tokenService.GetPayload(req.Token);

        var foundVerification = await _context
            .EmailVerifications
            .Where(v => v.UserId == payload.Id)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EmailVerificationNotFoundException();

        var foundUser = await _context
            .Users
            .FindAsync([payload.Id], cancellationToken)
            ?? throw new UserNotFoundException();

        if (foundUser.Id != foundVerification.UserId)
        {
            throw new UnauthorizedAccessException("Only account owner can search its verification.");
        }

        return foundVerification;
    }
}