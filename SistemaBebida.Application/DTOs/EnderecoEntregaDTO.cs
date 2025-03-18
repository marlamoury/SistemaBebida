using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Application.DTOs
{
    public class EnderecoEntregaDTO
    {
        [Required(ErrorMessage = "Rua é obrigatória")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(2, ErrorMessage = "Estado deve ter 2 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido")]
        public string CEP { get; set; }

        public string Complemento { get; set; }
    }
}
