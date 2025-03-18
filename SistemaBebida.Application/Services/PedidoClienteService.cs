using SistemaBebida.Domain.Entities;
using SistemaBebida.Domain.Repositories;
using SistemaBebida.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaBebida.Application.Services
{
    public class PedidoClienteService
    {
        private readonly IPedidoClienteRepository _pedidoClienteRepository;
        private readonly PedidoClientePublisher _pedidoClientePublisher; // Adicionando o publisher

        public PedidoClienteService(IPedidoClienteRepository pedidoClienteRepository, PedidoClientePublisher pedidoClientePublisher)
        {
            _pedidoClienteRepository = pedidoClienteRepository;
            _pedidoClientePublisher = pedidoClientePublisher; // Inicializando
        }

        public async Task<IEnumerable<PedidoCliente>> GetAllAsync()
        {
            return await _pedidoClienteRepository.GetAllAsync();
        }

        public async Task<PedidoCliente?> GetByIdAsync(int id)
        {
            return await _pedidoClienteRepository.GetByIdAsync(id);
        }

        public async Task AdicionarPedidoAsync(PedidoCliente pedido)
        {
            await _pedidoClienteRepository.AddAsync(pedido);

            
            _pedidoClientePublisher.PublicarPedido(pedido);
        }
    }
}
