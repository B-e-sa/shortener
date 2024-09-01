using FluentValidation;
using Shortener.Src.Application.Common;

namespace Shortener.Src.Application.Url.Queries.FindUrlById
{
    public class FindUrlByidQueryValidator : AbstractValidator<FindUrlByIdQuery>
    {
        public FindUrlByidQueryValidator()
        {
            RuleFor(v => v.Id).SetValidator(new IdValidator());
        }
    }
}