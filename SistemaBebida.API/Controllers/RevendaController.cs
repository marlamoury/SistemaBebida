using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaBebida.Domain.Repositories;
using SistemaBebida.Application.DTOs;
using SistemaBebida.Domain.Entities;

namespace SistemaBebida.API.Controllers
{
    [ApiController]
    [Route("api/revendas")]
    public class RevendaController : ControllerBase
    {
        private readonly IRevendaRepository _repository;

        public RevendaController(IRevendaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarRevenda([FromBody] RevendaDTO revendaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenda = new Revenda
            {
                CNPJ = revendaDTO.CNPJ,
                RazaoSocial = revendaDTO.RazaoSocial,
                NomeFantasia = revendaDTO.NomeFantasia,
                Email = revendaDTO.Email,
                Telefones = revendaDTO.Telefones,
                Contatos = revendaDTO.Contatos.ConvertAll(c => new Contato { Nome = c.Nome, Email = c.Email, Telefone = c.Telefone }),
                EnderecosEntrega = revendaDTO.EnderecosEntrega.ConvertAll(e => new EnderecoEntrega
                {
                    Rua = e.Rua,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.CEP,
                    Complemento = e.Complemento
                })
            };

            await _repository.AddAsync(revenda);
            return CreatedAtAction(nameof(CadastrarRevenda), new { id = revenda.Id }, revenda);
        }

        [HttpGet]
        public async Task<IActionResult> ListarRevendas()
        {
            var revendas = await _repository.GetAllAsync();
            return Ok(revendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarRevenda(int id)
        {
            var revenda = await _repository.GetByIdAsync(id);
            if (revenda == null)
                return NotFound();

            return Ok(revenda);
        }
    }
}
