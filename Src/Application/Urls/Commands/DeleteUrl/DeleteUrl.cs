using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Urls;

namespace Shortener.Application.Urls.Commands.DeleteUrl;

public record DeleteUrlCommand(int Id, string Token) : IRequest;

public class DeleteUrlCommandHandler(IAppDbContext context, ITokenService tokenService) : IRequestHandler<DeleteUrlCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task Handle(DeleteUrlCommand req, CancellationToken cancellationToken)
    {
        var payload = _tokenService.GetPayload(req.Token);

        var foundUrl = await _context.Urls
            .FindAsync([req.Id], cancellationToken)
            ?? throw new UrlNotFoundException();

        if(foundUrl.UserId == null)
        {
            throw new UnauthorizedAccessException("Users cannot delete anonymously created URLs.");
        }

        if (payload.Id != foundUrl.UserId)
        {
            throw new UnauthorizedAccessException("Only the url owner can delete it.");
        }

        _context.Urls.Remove(foundUrl);
        await _context.SaveChangesAsync(cancellationToken);
    }
};
