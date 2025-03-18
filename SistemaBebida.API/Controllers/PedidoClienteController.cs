using Microsoft.AspNetCore.Mvc;
using SistemaBebida.Domain.Entities;
using SistemaBebida.Application.Services;
using SistemaBebida.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBebida.API.Controllers
{
    [ApiController]
    [Route("api/pedidos-clientes")]
    public class PedidoClienteController : ControllerBase
    {
        private readonly PedidoClienteService _pedidoClienteService;

        public PedidoClienteController(PedidoClienteService pedidoClienteService)
        {
            _pedidoClienteService = pedidoClienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoClienteDTO pedidoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pedido = new PedidoCliente
            {
                RevendaId = pedidoDTO.RevendaId,
                NomeCliente = pedidoDTO.NomeCliente,
                CNPJCliente = pedidoDTO.CNPJCliente,
                Itens = pedidoDTO.Itens.Select(i => new ItemPedido
                {
                    NomeProduto = i.NomeProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };

            pedido.ValorTotal = pedido.Itens.Sum(i => i.Total);

            Console.WriteLine("📢 PedidoClienteController: Enviando pedido para PedidoClienteService...");

            await _pedidoClienteService.AdicionarPedidoAsync(pedido); // Agora está chamando o serviço

            return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoClienteService.GetAllAsync(); // Chamando o serviço ao invés do repositório
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedido(int id)
        {
            var pedido = await _pedidoClienteService.GetByIdAsync(id);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }
    }
}
