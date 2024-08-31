using FluentValidation;

namespace Shortener.Application.Url.Queries.FindUrlById
{
    public class FindUrlByidQueryValidator : AbstractValidator<FindUrlByIdQuery>
    {
        public FindUrlByidQueryValidator()
        {
            RuleFor(v => v.Id)
                .Custom((x, context) =>
                {
                    if (!int.TryParse(x, out int value) || value < 0)
                        context.AddFailure($"{x} is not a valid id number.");
                });
        }
    }
}