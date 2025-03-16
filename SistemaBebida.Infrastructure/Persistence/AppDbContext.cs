using Microsoft.EntityFrameworkCore;
using SistemaBebida.Domain.Entities;


namespace SistemaBebida.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Revenda> Revendas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
    }
}
