using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Application.Users.Commands.Register;

public record RegisterCommand : IRequest<string>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}

class RegisterCommandHandler(
    IAppDbContext context, 
    IJwtProvider jwt, 
    IEncryptionProvider encryption,
    IMailingProvider mailingProvider
) : IRequestHandler<RegisterCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwt = jwt;
    private readonly IEncryptionProvider _encryption = encryption;
    private readonly IMailingProvider _mailingProvider = mailingProvider;

    public async Task<string> Handle(RegisterCommand req, CancellationToken cancellationToken)
    {
        var trimmedEmail = req.Email.Trim();

        var duplicatedEmail = await _context
            .Users
            .AnyAsync(u => u.Email == trimmedEmail, cancellationToken);

        if (duplicatedEmail)
        {
            throw new DuplicatedUserCredentialsException("Email");
        }

        var trimmedUsername = req.Username.Trim();

        var duplicatedUsername = await _context
            .Users
            .AnyAsync(u => u.Username == trimmedUsername, cancellationToken);

        if (duplicatedUsername)
        {
            throw new DuplicatedUserCredentialsException("Username");
        }

        User user = new() 
        {
            Email = trimmedEmail,
            Username = trimmedUsername,
            Password = _encryption.Hash(req.Password.Trim()),
        };

        _context.Users.Add(user);

        EmailVerification verification = new()
        {
            User = user
        };

        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync(cancellationToken);

        //TODO: Uncomment
        //await _mailingProvider.SendVerificationCode(
        //    req.Username, 
        //    req.Email, 
        //    verification.Code);

        return _jwt.Generate(user);
    }
}