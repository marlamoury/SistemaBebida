using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaBebida.Domain.Entities
{
    public class Revenda
    {
        public int Id { get; set; }

        [Required]
        public string CNPJ { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        public string NomeFantasia { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public List<string> Telefones { get; set; } = new List<string>();

        [Required]
        public List<Contato> Contatos { get; set; } = new List<Contato>();
    }
}
