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

public class CreateUrlCommandHandler(
    IAppDbContext context, 
    IJwtProvider jwtProvider) : IRequestHandler<CreateUrlCommand, Url>
{
    private readonly IAppDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Url> Handle(CreateUrlCommand req, CancellationToken cancellationToken)
    {
        Url newUrl = new()
        {
            Title = req.Title,
            OriginalUrl = req.Url,
        };

        if (req.Token != null)
        {
            if(_jwtProvider.Validate(req.Token))
            {
                var payload = _jwtProvider.Read(req.Token);

                var foundUser = await _context
                    .Users
                    .FindAsync([payload.Id], cancellationToken)
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