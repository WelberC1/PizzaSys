using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class Produto
    {
        private Produto() { }

        public Produto(string nome, string descricao, decimal precoVenda, decimal custoProducao, Guid categoriaProdutoId)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do produto e obrigatorio.", nameof(nome));
            if (categoriaProdutoId == Guid.Empty) throw new ArgumentException("Categoria do produto e obrigatoria.", nameof(categoriaProdutoId));
            ValidarCusto(custoProducao);

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Descricao = descricao?.Trim() ?? string.Empty;
            PrecoVenda = precoVenda;
            CustoProducao = custoProducao;
            CategoriaProdutoId = categoriaProdutoId;
            Ativo = true;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal PrecoVenda { get; private set; }
        public decimal CustoProducao { get; private set; }
        public Guid CategoriaProdutoId { get; private set; }
        public bool Ativo { get; private set; }

        public CategoriaProduto? CategoriaProduto { get; private set; }

        public void Atualizar(string nome, string descricao, decimal precoVenda, decimal custoProducao, Guid categoriaProdutoId)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do produto e obrigatorio.", nameof(nome));
            if (categoriaProdutoId == Guid.Empty) throw new ArgumentException("Categoria do produto e obrigatoria.", nameof(categoriaProdutoId));
            ValidarCusto(custoProducao);

            Nome = nome.Trim();
            Descricao = descricao?.Trim() ?? string.Empty;
            PrecoVenda = precoVenda;
            CustoProducao = custoProducao;
            CategoriaProdutoId = categoriaProdutoId;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        private static void ValidarCusto(decimal custoProducao)
        {
            if (custoProducao < 0) throw new InvalidOperationException("Custo de producao nao pode ser negativo.");
        }
    }

}
