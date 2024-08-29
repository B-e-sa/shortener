using FluentValidation;
using Shortener.Application.Url.Queries.FindUrlByShortUrl;

namespace Shortener.Application.Url.Commands.FindUrlByShortUrl
{

    public class CreateUrlCommandValidator : AbstractValidator<FindUrlByShortUrlQuery>
    {
        public CreateUrlCommandValidator()
        {
            RuleFor(v => v.ShortUrl)
                .Length(4);
        }
    }
}