using MediatR;
using Shortener.Application.Common.Interfaces;

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

    public async Task<string> Handle(CreateUserCommand req, CancellationToken cancellationToken)
    {
        User user = new() 
        {
            Email = req.Email,
            Username = req.Username,
            Password = _encryption.Hash(req.Password),
        };

        _context.Users.Add(user);

        EmailVerification verification = new()
        {
            User = user
        };

        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync(cancellationToken);

        await _mailingProvider.SendVerificationCode(
            req.Username, 
            req.Email, 
            verification.Code);

        return _jwt.Generate(user);
    }
}