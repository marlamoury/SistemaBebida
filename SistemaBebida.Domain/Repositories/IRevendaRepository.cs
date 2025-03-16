using SistemaBebida.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBebida.Domain.Repositories
{
    public interface IRevendaRepository
    {
        Task<IEnumerable<Revenda>> GetAllAsync();
        Task<Revenda?> GetByIdAsync(int id);
        Task AddAsync(Revenda revenda);
        Task UpdateAsync(Revenda revenda);
        Task DeleteAsync(int id);
    }
}
