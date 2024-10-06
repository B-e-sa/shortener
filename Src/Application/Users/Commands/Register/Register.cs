using MediatR;
using Shortener.Application.Common.Interfaces;

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
        User user = new() 
        {
            Email = req.Email.Trim(),
            Username = req.Username.Trim(),
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