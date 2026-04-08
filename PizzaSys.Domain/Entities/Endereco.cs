using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class Endereco
    {
        private Endereco() { }

        public Endereco(Guid usuarioId, string rua, string numero, string bairro, string cidade, string estado, string cep)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentException("Usuario invalido.", nameof(usuarioId));

            if (string.IsNullOrWhiteSpace(rua) ||
                string.IsNullOrWhiteSpace(numero) ||
                string.IsNullOrWhiteSpace(bairro) ||
                string.IsNullOrWhiteSpace(cidade) ||
                string.IsNullOrWhiteSpace(estado) ||
                string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException("Todos os campos de endereco sao obrigatorios.");
            }

            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Rua = rua.Trim();
            Numero = numero.Trim();
            Bairro = bairro.Trim();
            Cidade = cidade.Trim();
            Estado = estado.Trim();
            CEP = cep.Trim();
        }

        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Rua { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Bairro { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;
        public string CEP { get; private set; } = string.Empty;

        public Usuario? Usuario { get; private set; }
    }
}
