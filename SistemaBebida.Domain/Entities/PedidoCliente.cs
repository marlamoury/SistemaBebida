using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Domain.Entities
{
    public class PedidoCliente
    {
        public int Id { get; set; }
        public int RevendaId { get; set; } // ID da revenda que está vendendo o pedido
        public Revenda Revenda { get; set; } // Relacionamento com Revenda
        public string NomeCliente { get; set; } // Cliente final
        public string CNPJCliente { get; set; } // Identificação do cliente
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public List<ItemPedido> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }

        // Adicionando ClientId
        public int ClienteId { get; set; }
    }


    public class ItemPedido
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Total => Quantidade * PrecoUnitario;
    }
}
