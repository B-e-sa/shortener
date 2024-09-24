using MediatR;
using Shortener.Application.Common;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Urls.Commands.CreateUrl;

public record CreateUrlCommand : IRequest<Url>
{
    public string Url { get; init; }
    public string Title { get; init; }
    public string? Token { get; init; }
}

public class CreateUrlCommandHandler(
    IAppDbContext context,
    ITokenService tokenService
    ) : IRequestHandler<CreateUrlCommand, Url>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Url> Handle(CreateUrlCommand req, CancellationToken cancellationToken)
    {
        Url newUrl = new()
        {
            Title = req.Title,
            OriginalUrl = req.Url,
        };

        if (req.Token != null)
        {
            var payload = _tokenService.GetPayload(req.Token);

            var foundUser = await _context
                .Users
                .FindAsync([payload.Id], cancellationToken)
                ?? throw new UserNotFoundException();

            newUrl.User = foundUser;
        }

        _context.Urls.Add(newUrl);
        await _context.SaveChangesAsync(cancellationToken);

        return newUrl;
    }
}