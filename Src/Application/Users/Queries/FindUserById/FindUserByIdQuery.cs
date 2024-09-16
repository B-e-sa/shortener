using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;
using Shortener.Domain.Entities;

namespace Shortener.Application.Users.Queries.FindUserById;

public sealed record FindUserByIdQueryResponse(
    int Id,
    string Email,
    ICollection<Url> Urls,
    DateTime CreatedAt,
    DateTime UpdatedAt);

public sealed record FindUserByIdQuery(int Id) : IRequest<FindUserByIdQueryResponse>;

internal sealed class FindUserByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<FindUserByIdQuery, FindUserByIdQueryResponse>
{
    readonly IAppDbContext _context = context;

    public async Task<FindUserByIdQueryResponse> Handle(
        FindUserByIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUser = await _context.Users.FindAsync([req.Id], cancellationToken: cancellationToken) 
            ?? throw new UserNotFoundException();

        var res = new FindUserByIdQueryResponse(
            foundUser.Id,
            foundUser.Email,
            foundUser.Urls,
            foundUser.CreatedAt,
            foundUser.UpdatedAt);

        return res;
    }
}