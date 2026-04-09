using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaSys.Domain.Entities;

namespace PizzaSys.Infra.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(usuario => usuario.Id);

        builder.Property(usuario => usuario.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(usuario => usuario.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(usuario => usuario.SenhaHash)
            .IsRequired();

        builder.Property(usuario => usuario.TipoUsuario)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(usuario => usuario.Ativo)
            .IsRequired();

        builder.HasIndex(usuario => usuario.Email)
            .IsUnique();

        builder.HasMany(usuario => usuario.Enderecos)
            .WithOne(endereco => endereco.Usuario)
            .HasForeignKey(endereco => endereco.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Pedido>()
            .WithOne(pedido => pedido.Usuario)
            .HasForeignKey(pedido => pedido.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
