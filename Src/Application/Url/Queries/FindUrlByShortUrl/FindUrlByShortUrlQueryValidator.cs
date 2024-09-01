using FluentValidation;

namespace Shortener.Src.Application.Url.Queries.FindUrlByShortUrl
{
    public class FindUrlByShortUrlQueryValidator : AbstractValidator<FindUrlByShortUrlQuery>
    {
        public FindUrlByShortUrlQueryValidator()
        {
            RuleFor(v => v.ShortUrl)
                .Length(4);
        }
    }
}