using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("PedidoItens");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Quantidade)
            .IsRequired();

        builder.Property(item => item.PrecoUnitario)
            .HasPrecision(18, 2);

        builder.Property(item => item.CustoUnitario)
            .HasPrecision(18, 2);

        builder.HasOne(item => item.Pedido)
            .WithMany(pedido => pedido.Itens)
            .HasForeignKey(item => item.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(item => item.Produto)
            .WithMany()
            .HasForeignKey(item => item.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
