using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaBebida.Domain.Entities
{
    public class Revenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ inválido")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Razão Social é obrigatória")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Nome Fantasia é obrigatório")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public List<string> Telefones { get; set; } = new List<string>();

        [Required(ErrorMessage = "Pelo menos um contato deve ser informado")]
        public List<Contato> Contatos { get; set; } = new List<Contato>();

        [Required(ErrorMessage = "Pelo menos um endereço de entrega deve ser informado")]
        public List<EnderecoEntrega> EnderecosEntrega { get; set; } = new List<EnderecoEntrega>();


    }
}
