using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PizzaSys.Application.Validators;
using PizzaSys.Domain.Entities;
using PizzaSys.Infra.Context;

namespace PizzaSys.Application.Services;

public class ProdutoService(PizzaSysDbContext dbContext)
{
    private readonly ProdutoValidator _validator = new();

    public async Task<Produto> CriarProduto(
        string nome,
        string descricao,
        decimal precoVenda,
        decimal custoProducao,
        Guid categoriaProdutoId,
        CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow((nome, precoVenda, custoProducao, categoriaProdutoId));

        var categoriaExiste = await dbContext.CategoriasProduto.AnyAsync(x => x.Id == categoriaProdutoId, cancellationToken);
        if (!categoriaExiste)
        {
            throw new InvalidOperationException("Categoria nao encontrada.");
        }

        var produto = new Produto(nome, descricao, precoVenda, custoProducao, categoriaProdutoId);

        dbContext.Produtos.Add(produto);
        await dbContext.SaveChangesAsync(cancellationToken);

        return produto;
    }

    public async Task AtualizarProduto(
        Guid produtoId,
        string nome,
        string descricao,
        decimal precoVenda,
        decimal custoProducao,
        Guid categoriaProdutoId,
        CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow((nome, precoVenda, custoProducao, categoriaProdutoId));

        var produto = await dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId, cancellationToken)
            ?? throw new InvalidOperationException("Produto nao encontrado.");

        var categoriaExiste = await dbContext.CategoriasProduto.AnyAsync(x => x.Id == categoriaProdutoId, cancellationToken);
        if (!categoriaExiste)
        {
            throw new InvalidOperationException("Categoria nao encontrada.");
        }

        produto.Atualizar(nome, descricao, precoVenda, custoProducao, categoriaProdutoId);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Ativar(Guid produtoId, CancellationToken cancellationToken = default)
    {
        var produto = await dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId, cancellationToken)
            ?? throw new InvalidOperationException("Produto nao encontrado.");

        produto.Ativar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Desativar(Guid produtoId, CancellationToken cancellationToken = default)
    {
        var produto = await dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId, cancellationToken)
            ?? throw new InvalidOperationException("Produto nao encontrado.");

        produto.Desativar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Produto>> ListarCardapio(CancellationToken cancellationToken = default)
    {
        return dbContext.Produtos
            .Where(x => x.Ativo)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Produto>> ListarPorCategoria(Guid categoriaProdutoId, CancellationToken cancellationToken = default)
    {
        return dbContext.Produtos
            .Where(x => x.CategoriaProdutoId == categoriaProdutoId)
            .ToListAsync(cancellationToken);
    }
}
