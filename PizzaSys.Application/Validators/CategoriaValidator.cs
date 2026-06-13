using FluentValidation;

namespace PizzaSys.Application.Validators;

public class CategoriaValidator : AbstractValidator<string>
{
    public CategoriaValidator()
    {
        RuleFor(x => x)
            .NotEmpty();
    }
}
