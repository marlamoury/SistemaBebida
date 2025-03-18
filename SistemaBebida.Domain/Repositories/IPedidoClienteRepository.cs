using SistemaBebida.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaBebida.Domain.Repositories
{
    public interface IPedidoClienteRepository
    {
        Task<IEnumerable<PedidoCliente>> GetAllAsync();
        Task<PedidoCliente?> GetByIdAsync(int id);
        Task AddAsync(PedidoCliente pedido);
        Task SaveChangesAsync(); // Adicionado para garantir persistência
    }
}
