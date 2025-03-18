using SistemaBebida.Domain.Entities;
using SistemaBebida.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaBebida.Infrastructure.Persistence
{
    public class PedidoClienteRepository : IPedidoClienteRepository
    {
        private readonly AppDbContext _context;

        public PedidoClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoCliente>> GetAllAsync()
        {
            return await _context.PedidosClientes.Include(p => p.Itens).ToListAsync();
        }

        public async Task<PedidoCliente?> GetByIdAsync(int id)
        {
            return await _context.PedidosClientes
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(PedidoCliente pedido)
        {
            await _context.PedidosClientes.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
