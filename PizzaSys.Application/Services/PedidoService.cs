using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PizzaSys.Application.Validators;
using PizzaSys.Domain.Entities;
using PizzaSys.Domain.Enums;
using PizzaSys.Infra.Context;

namespace PizzaSys.Application.Services;

public class PedidoService(PizzaSysDbContext dbContext)
{
    private readonly PedidoValidator _validator = new();

    public async Task<Pedido> CriarPedido(Guid usuarioId, Guid enderecoId, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow((usuarioId, enderecoId, null, null));

        var usuarioExiste = await dbContext.Usuarios.AnyAsync(x => x.Id == usuarioId, cancellationToken);
        if (!usuarioExiste)
        {
            throw new InvalidOperationException("Usuario nao encontrado.");
        }

        var enderecoExiste = await dbContext.Enderecos.AnyAsync(x => x.Id == enderecoId && x.UsuarioId == usuarioId, cancellationToken);
        if (!enderecoExiste)
        {
            throw new InvalidOperationException("Endereco nao encontrado para o usuario informado.");
        }

        var pedido = new Pedido(usuarioId, enderecoId);

        dbContext.Pedidos.Add(pedido);
        await dbContext.SaveChangesAsync(cancellationToken);

        return pedido;
    }

    public async Task AdicionarItem(Guid pedidoId, Guid produtoId, int quantidade, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow((Guid.Empty, Guid.Empty, produtoId, quantidade));

        var pedido = await dbContext.Pedidos
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id == pedidoId, cancellationToken)
            ?? throw new InvalidOperationException("Pedido nao encontrado.");

        var produto = await dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId, cancellationToken)
            ?? throw new InvalidOperationException("Produto nao encontrado.");

        pedido.AdicionarItem(produto, quantidade);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarStatus(Guid pedidoId, PedidoStatus status, CancellationToken cancellationToken = default)
    {
        var pedido = await dbContext.Pedidos
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id == pedidoId, cancellationToken)
            ?? throw new InvalidOperationException("Pedido nao encontrado.");

        pedido.AtualizarStatus(status);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelarPedido(Guid pedidoId, CancellationToken cancellationToken = default)
    {
        var pedido = await dbContext.Pedidos
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id == pedidoId, cancellationToken)
            ?? throw new InvalidOperationException("Pedido nao encontrado.");

        pedido.Cancelar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RegistrarPerda(Guid pedidoId, string motivo, CancellationToken cancellationToken = default)
    {
        var pedido = await dbContext.Pedidos
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id == pedidoId, cancellationToken)
            ?? throw new InvalidOperationException("Pedido nao encontrado.");

        pedido.RegistrarPerda(motivo);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Pedido>> ListarPorCliente(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return dbContext.Pedidos
            .Include(x => x.Itens)
            .Where(x => x.UsuarioId == usuarioId)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Pedido>> ListarPedidosAbertos(CancellationToken cancellationToken = default)
    {
        return dbContext.Pedidos
            .Include(x => x.Itens)
            .Where(x => x.Status != PedidoStatus.Entregue && x.Status != PedidoStatus.Cancelado)
            .ToListAsync(cancellationToken);
    }
}
