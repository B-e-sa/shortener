using FluentValidation;

namespace Shortener.Src.Application.Common
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(id => id)
                .GreaterThan(0).WithMessage("ID must be a positive integer.");
        }
    }
}