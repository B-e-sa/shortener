using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Url.Queries.FindUrlById;

public class FindUrlByidQueryValidator : AbstractValidator<FindUrlByIdQuery>
{
    public FindUrlByidQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}
