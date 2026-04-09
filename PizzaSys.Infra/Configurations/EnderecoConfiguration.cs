using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(endereco => endereco.Id);

        builder.Property(endereco => endereco.Rua)
            .IsRequired();

        builder.Property(endereco => endereco.Numero)
            .IsRequired();

        builder.Property(endereco => endereco.Bairro)
            .IsRequired();

        builder.Property(endereco => endereco.Cidade)
            .IsRequired();

        builder.Property(endereco => endereco.Estado)
            .IsRequired();

        builder.Property(endereco => endereco.CEP)
            .IsRequired()
            .HasMaxLength(8)
            .IsFixedLength();

        builder.HasOne(endereco => endereco.Usuario)
            .WithMany(usuario => usuario.Enderecos)
            .HasForeignKey(endereco => endereco.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Pedido>()
            .WithOne(pedido => pedido.Endereco)
            .HasForeignKey(pedido => pedido.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
