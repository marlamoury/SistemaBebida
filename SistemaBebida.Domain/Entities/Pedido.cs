using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Domain.Entities
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public string? Observacao { get; set; } // Campo opcional para observações
    }
}
