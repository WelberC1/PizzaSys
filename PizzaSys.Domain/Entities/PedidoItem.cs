using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSys.Domain.Entities
{
    public class PedidoItem
    {
        private PedidoItem() { }

        public PedidoItem(Guid pedidoId, Guid produtoId, int quantidade, decimal precoUnitario, decimal custoUnitario)
        {
            if (pedidoId == Guid.Empty) throw new ArgumentException("Pedido invalido.", nameof(pedidoId));
            if (produtoId == Guid.Empty) throw new ArgumentException("Produto invalido.", nameof(produtoId));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.", nameof(quantidade));
            if (custoUnitario < 0) throw new InvalidOperationException("Custo unitario nao pode ser negativo.");

            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            CustoUnitario = custoUnitario;
        }

        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal CustoUnitario { get; private set; }

        public Pedido? Pedido { get; private set; }
        public Produto? Produto { get; private set; }

        public decimal ValorTotalItem() => Quantidade * PrecoUnitario;
        public decimal CustoTotalItem() => Quantidade * CustoUnitario;
    }

}
