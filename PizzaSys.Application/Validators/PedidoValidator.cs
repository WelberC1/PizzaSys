using FluentValidation;

namespace PizzaSys.Application.Validators;

public class PedidoValidator : AbstractValidator<(Guid UsuarioId, Guid EnderecoId, Guid? ProdutoId, int? Quantidade)>
{
    public PedidoValidator()
    {
        When(x => x.UsuarioId != Guid.Empty || x.EnderecoId != Guid.Empty, () =>
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty();

            RuleFor(x => x.EnderecoId)
                .NotEmpty();
        });

        When(x => x.ProdutoId.HasValue || x.Quantidade.HasValue, () =>
        {
            RuleFor(x => x.ProdutoId)
                .NotNull()
                .Must(x => x.HasValue && x.Value != Guid.Empty);

            RuleFor(x => x.Quantidade)
                .NotNull()
                .GreaterThan(0);
        });
    }
}
