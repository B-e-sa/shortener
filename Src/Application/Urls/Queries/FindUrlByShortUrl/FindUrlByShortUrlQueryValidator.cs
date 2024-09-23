using FluentValidation;

namespace Shortener.Application.Urls.Queries.FindUrlByShortUrl;

public class FindUrlByShortUrlQueryValidator : AbstractValidator<FindUrlByShortUrlQuery>
{
    public FindUrlByShortUrlQueryValidator()
    {
        RuleFor(v => v.ShortUrl).Length(4);
    }
}