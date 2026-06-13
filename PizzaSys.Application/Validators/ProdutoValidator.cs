using FluentValidation;

namespace PizzaSys.Application.Validators;

public class ProdutoValidator : AbstractValidator<(string Nome, decimal PrecoVenda, decimal CustoProducao, Guid CategoriaProdutoId)>
{
    public ProdutoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty();

        RuleFor(x => x.PrecoVenda)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CustoProducao)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoriaProdutoId)
            .NotEmpty();
    }
}
