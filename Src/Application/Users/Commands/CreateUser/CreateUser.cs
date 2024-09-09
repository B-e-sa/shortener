using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Users.Abstractions;
using Shortener.Domain.Entities;

namespace Shortener.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

class CreateUserCommandHandler(
    IAppDbContext context, 
    IJwtProvider jwt, 
    IEncryptionProvider encryption
) : IRequestHandler<CreateUserCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwt = jwt;
    private readonly IEncryptionProvider _encryption = encryption;

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            Email = request.Email,
            Username = request.Username,
            Password = _encryption.Hash(request.Password),
        };


        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        return _jwt.Generate(newUser);
    }
}