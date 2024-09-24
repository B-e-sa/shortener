using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Common.Models;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Queries.FindUserById;

public sealed record FindUserByIdQuery(int Id) : IRequest<UserDTO>;

internal sealed class FindUserByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<FindUserByIdQuery, UserDTO>
{
    readonly IAppDbContext _context = context;

    public async Task<UserDTO> Handle(
        FindUserByIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUser = await _context
            .Users
            .Where(u => u.Id == req.Id) 
            .Select(u => new UserDTO 
            { 
                Id = u.Id,
                UserName = u.Username,
                Email = u.Email,
                ConfirmedEmail = u.ConfirmedEmail,
            })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new UserNotFoundException();

        return foundUser;
    }
}