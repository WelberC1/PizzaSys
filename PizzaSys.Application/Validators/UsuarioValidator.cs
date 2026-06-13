using FluentValidation;

namespace PizzaSys.Application.Validators;

public class UsuarioValidator : AbstractValidator<(string Nome, string Email, string SenhaHash)>
{
    public UsuarioValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.SenhaHash)
            .NotEmpty();
    }
}
