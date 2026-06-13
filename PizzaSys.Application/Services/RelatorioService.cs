using Microsoft.EntityFrameworkCore;
using PizzaSys.Domain.Enums;
using PizzaSys.Infra.Context;

namespace PizzaSys.Application.Services;

public class RelatorioService(PizzaSysDbContext dbContext)
{
    public async Task<decimal> FaturamentoPorPeriodo(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default)
    {
        return await dbContext.Pedidos
            .Where(x => x.DataCriacao >= dataInicial && x.DataCriacao <= dataFinal && x.Status != PedidoStatus.Cancelado)
            .SumAsync(x => x.ValorTotal, cancellationToken);
    }

    public async Task<decimal> CustoTotal(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default)
    {
        var pedidos = await dbContext.Pedidos
            .Include(x => x.Itens)
            .Where(x => x.DataCriacao >= dataInicial && x.DataCriacao <= dataFinal && x.Status != PedidoStatus.Cancelado)
            .ToListAsync(cancellationToken);

        return pedidos.Sum(x => x.CalcularCustoTotal());
    }

    public async Task<decimal> LucroTotal(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default)
    {
        var pedidos = await dbContext.Pedidos
            .Include(x => x.Itens)
            .Where(x => x.DataCriacao >= dataInicial && x.DataCriacao <= dataFinal && x.Status != PedidoStatus.Cancelado)
            .ToListAsync(cancellationToken);

        return pedidos.Sum(x => x.CalcularLucro());
    }

    public async Task<decimal> TicketMedio(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default)
    {
        var pedidos = await dbContext.Pedidos
            .Where(x => x.DataCriacao >= dataInicial && x.DataCriacao <= dataFinal && x.Status != PedidoStatus.Cancelado)
            .ToListAsync(cancellationToken);

        return pedidos.Count == 0 ? 0 : pedidos.Average(x => x.ValorTotal);
    }

    public async Task<List<(Guid ProdutoId, string Nome, int QuantidadeVendida)>> ProdutosMaisVendidos(
        DateTime dataInicial,
        DateTime dataFinal,
        int quantidade = 5,
        CancellationToken cancellationToken = default)
    {
        var itens = await dbContext.PedidoItens
            .Include(x => x.Produto)
            .Include(x => x.Pedido)
            .Where(x => x.Pedido != null
                && x.Pedido.DataCriacao >= dataInicial
                && x.Pedido.DataCriacao <= dataFinal
                && x.Pedido.Status != PedidoStatus.Cancelado)
            .GroupBy(x => new { x.ProdutoId, Nome = x.Produto != null ? x.Produto.Nome : string.Empty })
            .Select(x => new
            {
                x.Key.ProdutoId,
                x.Key.Nome,
                QuantidadeVendida = x.Sum(item => item.Quantidade)
            })
            .OrderByDescending(x => x.QuantidadeVendida)
            .Take(quantidade)
            .ToListAsync(cancellationToken);

        return itens
            .Select(x => (x.ProdutoId, x.Nome, x.QuantidadeVendida))
            .ToList();
    }

    public async Task<List<(Guid CategoriaId, string Nome, int QuantidadeVendida)>> CategoriasMaisVendidas(
        DateTime dataInicial,
        DateTime dataFinal,
        int quantidade = 5,
        CancellationToken cancellationToken = default)
    {
        var categorias = await dbContext.PedidoItens
            .Include(x => x.Produto)
            .Include(x => x.Pedido)
            .Where(x => x.Pedido != null
                && x.Produto != null
                && x.Pedido.DataCriacao >= dataInicial
                && x.Pedido.DataCriacao <= dataFinal
                && x.Pedido.Status != PedidoStatus.Cancelado)
            .Join(
                dbContext.CategoriasProduto,
                item => item.Produto!.CategoriaProdutoId,
                categoria => categoria.Id,
                (item, categoria) => new
                {
                    categoria.Id,
                    categoria.Nome,
                    item.Quantidade
                })
            .GroupBy(x => new { x.Id, x.Nome })
            .Select(x => new
            {
                CategoriaId = x.Key.Id,
                x.Key.Nome,
                QuantidadeVendida = x.Sum(item => item.Quantidade)
            })
            .OrderByDescending(x => x.QuantidadeVendida)
            .Take(quantidade)
            .ToListAsync(cancellationToken);

        return categorias
            .Select(x => (x.CategoriaId, x.Nome, x.QuantidadeVendida))
            .ToList();
    }

    public async Task<List<dynamic>> RankingClientesFrequentes()
    {
        

    }
}
