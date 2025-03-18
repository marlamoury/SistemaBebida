using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Application.DTOs
{
    public class PedidoClienteDTO
    {
        [Required]
        public int RevendaId { get; set; }

        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "CNPJ do cliente é obrigatório")]
        public string CNPJCliente { get; set; }

        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }

    public class ItemPedidoDTO
    {
        [Required]
        public string NomeProduto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal PrecoUnitario { get; set; }
    }
}
