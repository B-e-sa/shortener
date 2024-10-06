using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using FluentValidation;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.Login;

public record LoginCommand : IRequest<string>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

class LoginCommandHandler(
    IAppDbContext context,
    IJwtProvider jwt,
    IEncryptionProvider encryptionProvider
) : IRequestHandler<LoginCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwt = jwt;
    private readonly IEncryptionProvider _encryptionProvider = encryptionProvider;

    public async Task<string> Handle(LoginCommand req, CancellationToken cancellationToken)
    {
        var foundUser = await _context.Users
             .Where(u => u.Email == req.Email.Trim())
             .FirstOrDefaultAsync(cancellationToken: cancellationToken) 
             ?? throw new UserNotFoundException();
        
        if (!_encryptionProvider.Verify(req.Password.Trim(), foundUser.Password))
        {
            throw new InvalidUserPasswordException();
        }

        return _jwt.Generate(foundUser);
    }
}
