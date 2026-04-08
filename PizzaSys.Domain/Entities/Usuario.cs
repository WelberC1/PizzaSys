using PizzaSys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class Usuario
    {
        private readonly List<Endereco> _enderecos = [];

        private Usuario() { }

        public Usuario(string nome, string email, string senhaHash, TipoUsuario tipoUsuario)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do usuario e obrigatorio.", nameof(nome));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email do usuario e obrigatorio.", nameof(email));
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("SenhaHash e obrigatoria.", nameof(senhaHash));

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Email = email.Trim().ToLowerInvariant();
            SenhaHash = senhaHash;
            TipoUsuario = tipoUsuario;
            Ativo = true;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;
        public TipoUsuario TipoUsuario { get; private set; }
        public bool Ativo { get; private set; }
        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.AsReadOnly();

        public void Desativar() => Ativo = false;
        public void Ativar() => Ativo = true;

        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco is null) throw new ArgumentNullException(nameof(endereco));
            _enderecos.Add(endereco);
        }
    }
}
