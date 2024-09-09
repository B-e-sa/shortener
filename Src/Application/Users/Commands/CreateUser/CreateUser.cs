using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Entities;

namespace Shortener.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<User>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

class CreateUserCommandHandler(IAppDbContext context) : IRequestHandler<CreateUserCommand, User>
{
    private readonly IAppDbContext _context = context;

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
        string hash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

        var newUser = new User
        {
            Email = request.Email,
            Username = request.Username,
            Password = hash,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}