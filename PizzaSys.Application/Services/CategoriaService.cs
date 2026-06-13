using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PizzaSys.Application.Validators;
using PizzaSys.Domain.Entities;
using PizzaSys.Infra.Context;

namespace PizzaSys.Application.Services;

public class CategoriaService(PizzaSysDbContext dbContext)
{
    private readonly CategoriaValidator _validator = new();

    public async Task<CategoriaProduto> CriarCategoria(string nome, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow(nome);

        var categoria = new CategoriaProduto(nome);

        dbContext.CategoriasProduto.Add(categoria);
        await dbContext.SaveChangesAsync(cancellationToken);

        return categoria;
    }

    public async Task Ativar(Guid categoriaId, CancellationToken cancellationToken = default)
    {
        var categoria = await dbContext.CategoriasProduto.FirstOrDefaultAsync(x => x.Id == categoriaId, cancellationToken)
            ?? throw new InvalidOperationException("Categoria nao encontrada.");

        categoria.Ativar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Desativar(Guid categoriaId, CancellationToken cancellationToken = default)
    {
        var categoria = await dbContext.CategoriasProduto.FirstOrDefaultAsync(x => x.Id == categoriaId, cancellationToken)
            ?? throw new InvalidOperationException("Categoria nao encontrada.");

        categoria.Desativar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<CategoriaProduto>> Listar(CancellationToken cancellationToken = default)
    {
        return dbContext.CategoriasProduto.ToListAsync(cancellationToken);
    }
}
