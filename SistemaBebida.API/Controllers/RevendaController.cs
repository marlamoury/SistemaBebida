using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaBebida.Domain.Entities;
using SistemaBebida.Infrastructure.Persistence;

namespace SistemaBebida.API.Controllers
{
    [ApiController]
    [Route("api/revendas")]
    public class RevendaController : ControllerBase
    {
        private readonly RevendaRepository _repository;

        public RevendaController(RevendaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarRevenda([FromBody] Revenda revenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
