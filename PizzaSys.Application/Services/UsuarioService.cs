using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PizzaSys.Application.Validators;
using PizzaSys.Domain.Entities;
using PizzaSys.Domain.Enums;
using PizzaSys.Infra.Context;

namespace PizzaSys.Application.Services;

public class UsuarioService(PizzaSysDbContext dbContext)
{
    private readonly UsuarioValidator _validator = new();

    public async Task<Usuario> CriarUsuario(string nome, string email, string senhaHash, TipoUsuario tipoUsuario, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow((nome, email, senhaHash));

        var usuario = new Usuario(nome, email, senhaHash, tipoUsuario);

        dbContext.Usuarios.Add(usuario);
        await dbContext.SaveChangesAsync(cancellationToken);

        return usuario;
    }

    public async Task DesativarUsuario(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId, cancellationToken)
            ?? throw new InvalidOperationException("Usuario nao encontrado.");

        usuario.Desativar();
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Usuario>> ListarPorTipo(TipoUsuario tipoUsuario, CancellationToken cancellationToken = default)
    {
        return dbContext.Usuarios
            .Where(x => x.TipoUsuario == tipoUsuario)
            .ToListAsync(cancellationToken);
    }
}
