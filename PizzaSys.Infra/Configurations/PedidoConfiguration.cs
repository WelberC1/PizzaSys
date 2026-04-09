using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(pedido => pedido.Id);

        builder.Property(pedido => pedido.UsuarioId)
            .IsRequired();

        builder.Property(pedido => pedido.EnderecoId)
            .IsRequired();

        builder.Property(pedido => pedido.DataCriacao)
            .IsRequired();

        builder.Property(pedido => pedido.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(pedido => pedido.Perda)
            .IsRequired();

        builder.Property(pedido => pedido.Visivel)
            .IsRequired();

        builder.Property(pedido => pedido.ValorTotal)
            .HasPrecision(18, 2);

        builder.HasOne(pedido => pedido.Usuario)
            .WithMany()
            .HasForeignKey(pedido => pedido.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pedido => pedido.Endereco)
            .WithMany()
            .HasForeignKey(pedido => pedido.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(pedido => pedido.Itens)
            .WithOne(item => item.Pedido)
            .HasForeignKey(item => item.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
