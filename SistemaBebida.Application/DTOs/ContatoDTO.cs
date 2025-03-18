using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Application.DTOs
{
    public class ContatoDTO
    {
        [Required(ErrorMessage = "Nome do contato é obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public string Telefone { get; set; }
    }
}
