using PizzaSys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class Pedido
    {
        private readonly List<PedidoItem> _itens = [];

        private Pedido() { }

        public Pedido(Guid usuarioId, Guid enderecoId)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentException("Cliente invalido.", nameof(usuarioId));
            if (enderecoId == Guid.Empty) throw new ArgumentException("Endereco invalido.", nameof(enderecoId));

            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            EnderecoId = enderecoId;
            DataCriacao = DateTime.UtcNow;
            Status = PedidoStatus.Criado;
            ValorTotal = 0;
        }

        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataEntrega { get; private set; }
        public DateTime? DataCancelamento { get; private set; }
        public PedidoStatus Status { get; private set; }
        public bool Perda { get; private set; }
        public string MotivoPerda { get; private set; } = string.Empty;
        public decimal ValorTotal { get; private set; }
        public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

        public Usuario? Usuario { get; private set; }
        public Guid EnderecoId { get; private set; }
        public Endereco? Endereco { get; private set; }
        public bool Visivel { get; private set; } = true;
        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto is null) throw new ArgumentNullException(nameof(produto));
            if (!produto.Ativo) throw new InvalidOperationException("Produto inativo nao pode ser adicionado ao pedido.");
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.", nameof(quantidade));

            var item = new PedidoItem(Id, produto.Id, quantidade, produto.PrecoVenda, produto.CustoProducao);
            _itens.Add(item);
            RecalcularValorTotal();
        }

        public void AtualizarStatus(PedidoStatus novoStatus)
        {
            if (Status == PedidoStatus.Cancelado || Status == PedidoStatus.Entregue)
                throw new InvalidOperationException("Pedido finalizado nao permite alteracao de status.");

            var transicaoValida =
                (Status == PedidoStatus.Criado && novoStatus == PedidoStatus.EmPreparacao) ||
                (Status == PedidoStatus.EmPreparacao && novoStatus == PedidoStatus.Pronto) ||
                (Status == PedidoStatus.Pronto && novoStatus == PedidoStatus.SaiuParaEntrega) ||
                (Status == PedidoStatus.SaiuParaEntrega && novoStatus == PedidoStatus.Entregue);

            if (!transicaoValida) throw new InvalidOperationException("Transicao de status invalida.");

            Status = novoStatus;

            if (novoStatus == PedidoStatus.Entregue)
            {
                DataEntrega = DateTime.UtcNow;
            }
        }

        public void Cancelar()
        {
            if (Status == PedidoStatus.SaiuParaEntrega || Status == PedidoStatus.Entregue)
                throw new InvalidOperationException("Nao e permitido cancelar pedido apos sair para entrega.");

            Status = PedidoStatus.Cancelado;
            DataCancelamento = DateTime.UtcNow;
        }

        public void RegistrarPerda(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("Motivo da perda e obrigatorio.", nameof(motivo));

            Perda = true;
            MotivoPerda = motivo.Trim();
        }

        public decimal CalcularCustoTotal() => _itens.Sum(item => item.CustoTotalItem());
        public decimal CalcularLucro() => ValorTotal - CalcularCustoTotal();

        public void ValidarPedidoComItens()
        {
            if (_itens.Count == 0) throw new InvalidOperationException("Pedido deve possuir ao menos um item.");
        }

        private void RecalcularValorTotal() => ValorTotal = _itens.Sum(item => item.ValorTotalItem());
    }
}
