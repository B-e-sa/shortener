using MediatR;
using Shortener.Application.Common;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Entities;

namespace Shortener.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}

class CreateUserCommandHandler(
    IAppDbContext context, 
    IJwtProvider jwt, 
    IEncryptionProvider encryption,
    IMailingProvider mailingProvider
) : IRequestHandler<CreateUserCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwt = jwt;
    private readonly IEncryptionProvider _encryption = encryption;
    private readonly IMailingProvider _mailingProvider = mailingProvider;

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new() 
        {
            Email = request.Email,
            Username = request.Username,
            Password = _encryption.Hash(request.Password),
        };

        _context.Users.Add(user);

        EmailVerification verification = new()
        {
            User = user
        };

        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync(cancellationToken);

        await _mailingProvider.SendVerificationCode(
            request.Username, 
            request.Email, 
            verification.Code);

        return _jwt.Generate(user);
    }
}