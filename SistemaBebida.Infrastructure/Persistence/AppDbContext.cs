using Microsoft.EntityFrameworkCore;
using SistemaBebida.Domain.Entities;


namespace SistemaBebida.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
      

        public DbSet<Revenda> Revendas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<PedidoCliente> PedidosClientes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PedidoCliente>()
                .HasKey(p => p.Id);
        }

    }
}
