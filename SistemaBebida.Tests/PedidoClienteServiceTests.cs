using Moq;
using Xunit;
using SistemaBebida.Domain.Repositories;
using SistemaBebida.Application.Services;
using SistemaBebida.Infrastructure.Messaging; // Adicione essa linha caso não esteja presente

namespace SistemaBebida.Tests.Services
{
    public class PedidoClienteServiceTests
    {
        private readonly Mock<IPedidoClienteRepository> _pedidoClienteRepositoryMock;
        private readonly Mock<PedidoClientePublisher> _pedidoClientePublisherMock; // Adicionado Mock do Publisher
        private readonly PedidoClienteService _pedidoClienteService;

        public PedidoClienteServiceTests()
        {
            _pedidoClienteRepositoryMock = new Mock<IPedidoClienteRepository>();
            _pedidoClientePublisherMock = new Mock<PedidoClientePublisher>(); // Criando Mock do Publisher

            // Agora passamos ambos os mocks no construtor do serviço
            _pedidoClienteService = new PedidoClienteService(_pedidoClienteRepositoryMock.Object, _pedidoClientePublisherMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDePedidos()
        {
            // Testes continuam normalmente
        }
    }
}
