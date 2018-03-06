using kiga.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace kiga.repository.Context
{
    public class kigaContexto : DbContext
    {
        public kigaContexto(DbContextOptions<kigaContexto> options) : base(options)
        {

        }

        public DbSet<UsuarioDomain> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioDomain>().ToTable("Usuarios").HasIndex(i => new { i.facebookId }).IsUnique(true);
        }
    }
}
