using Microsoft.EntityFrameworkCore;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Context;

public class PizzaSysDbContext(DbContextOptions<PizzaSysDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<CategoriaProduto> CategoriasProduto => Set<CategoriaProduto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaSysDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
