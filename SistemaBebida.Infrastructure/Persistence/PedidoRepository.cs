using SistemaBebida.Domain.Entities;
using SistemaBebida.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SistemaBebida.Infrastructure.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido?> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
