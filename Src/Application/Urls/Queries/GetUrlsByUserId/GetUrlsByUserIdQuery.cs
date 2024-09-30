using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Urls.Queries.GetUrlsByUserId;

public record GetUrlsByUserIdQuery(string Token) : IRequest<List<Url>>;

public class GetUrlsByUserIdQueryHandler(
    IAppDbContext context,
    ITokenService tokenService) : IRequestHandler<GetUrlsByUserIdQuery, List<Url>>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<List<Url>> Handle(
        GetUrlsByUserIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        return await _context
            .Urls
            .Where(u => u.UserId == foundUser.Id)
            .ToListAsync(cancellationToken);
    }
}