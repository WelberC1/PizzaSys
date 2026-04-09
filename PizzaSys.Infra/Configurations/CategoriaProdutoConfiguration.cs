using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class CategoriaProdutoConfiguration : IEntityTypeConfiguration<CategoriaProduto>
{
    public void Configure(EntityTypeBuilder<CategoriaProduto> builder)
    {
        builder.ToTable("CategoriasProduto");

        builder.HasKey(categoria => categoria.Id);

        builder.Property(categoria => categoria.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(categoria => categoria.Ativo)
            .IsRequired();

        builder.HasMany<Produto>()
            .WithOne(produto => produto.CategoriaProduto)
            .HasForeignKey(produto => produto.CategoriaProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
