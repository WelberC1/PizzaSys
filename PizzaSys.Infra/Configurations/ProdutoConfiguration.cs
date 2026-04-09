using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(produto => produto.Id);

        builder.Property(produto => produto.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(produto => produto.Descricao)
            .HasMaxLength(500);

        builder.Property(produto => produto.PrecoVenda)
            .HasPrecision(18, 2);

        builder.Property(produto => produto.CustoProducao)
            .HasPrecision(18, 2);

        builder.Property(produto => produto.Ativo)
            .IsRequired();

        builder.HasOne(produto => produto.CategoriaProduto)
            .WithMany()
            .HasForeignKey(produto => produto.CategoriaProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<PedidoItem>()
            .WithOne(item => item.Produto)
            .HasForeignKey(item => item.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
