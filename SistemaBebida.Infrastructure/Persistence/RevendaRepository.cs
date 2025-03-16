using SistemaBebida.Domain.Entities;
using SistemaBebida.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaBebida.Infrastructure.Persistence
{
    public class RevendaRepository : IRevendaRepository
    {
        private readonly AppDbContext _context;

        public RevendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Revenda>> GetAllAsync()
        {
            return await _context.Revendas.ToListAsync();
        }

        public async Task<Revenda?> GetByIdAsync(int id)
        {
            return await _context.Revendas.FindAsync(id);
        }

        public async Task AddAsync(Revenda revenda)
        {
            await _context.Revendas.AddAsync(revenda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Revenda revenda)
        {
            _context.Revendas.Update(revenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var revenda = await _context.Revendas.FindAsync(id);
            if (revenda != null)
            {
                _context.Revendas.Remove(revenda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
