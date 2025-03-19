using SistemaBebida.Application.Clientes;
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
        private readonly PedidoClientePublisher _pedidoClientePublisher;
        private readonly FornecedorApiClient _fornecedorApiClient;


        public PedidoClienteService(
            IPedidoClienteRepository pedidoClienteRepository,
            PedidoClientePublisher pedidoClientePublisher,
            FornecedorApiClient fornecedorApiClient) 
        {
            _pedidoClienteRepository = pedidoClienteRepository;
            _pedidoClientePublisher = pedidoClientePublisher;
            _fornecedorApiClient = fornecedorApiClient; 
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

        public async Task<string> EnviarPedidoParaFornecedorAsync(PedidoCliente pedido)
        {
            int totalQuantidade = pedido.Itens.Sum(i => i.Quantidade);

            if (totalQuantidade < 1000)
            {
                throw new Exception("Pedido não pode ser enviado: quantidade mínima de 1000 unidades.");
            }

            return await _fornecedorApiClient.CriarPedidoAsync(pedido);
        }


    }
}
