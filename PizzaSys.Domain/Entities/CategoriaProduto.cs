using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class CategoriaProduto
    {
        private CategoriaProduto() { }

        public CategoriaProduto(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome da categoria e obrigatorio.", nameof(nome));

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Ativo = true;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
    }

}
