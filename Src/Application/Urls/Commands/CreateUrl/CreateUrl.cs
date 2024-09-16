using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions;
using Shortener.Domain.Common.Exceptions.Users;
using Shortener.Domain.Entities;

namespace Shortener.Application.Urls.Commands.CreateUrl;

public record CreateUrlCommand : IRequest<Url>
{
    public string Url { get; init; }
    public string Title { get; init; }
    public string? Token { get; init; }
}

class CreateUrlCommandHandler(IAppDbContext context, IJwtProvider jwtProvider) : IRequestHandler<CreateUrlCommand, Url>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private static string GenerateShortUrl()
    {
        string shortUrl = "";
        string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGIJKLMNOPQRSTUVWXYZ0123456789";

        Random random = new();
        for (int i = 0; i < 4; i++)
        {
            int index = random.Next(0, chars.Length);
            shortUrl += chars[index];
        }
        return shortUrl;
    }

    public async Task<Url> Handle(CreateUrlCommand req, CancellationToken cancellationToken)
    {
        Url newUrl = new()
        {
            Title = req.Title,
            OriginalUrl = req.Url,
            ShortUrl = GenerateShortUrl(),
        };

        if (req.Token != null)
        {
            if(_jwtProvider.Validate(req.Token))
            {
                var payload = _jwtProvider.Read(req.Token);

                var foundUser = await _context.Users.FindAsync([payload.Id], cancellationToken: cancellationToken)
                ?? throw new UserNotFoundException();

                newUrl.User = foundUser;
            }
            else
            {
                throw new InvalidTokenException();
            }
        }

        _context.Urls.Add(newUrl);
        await _context.SaveChangesAsync(cancellationToken);

        return newUrl;
    }
}