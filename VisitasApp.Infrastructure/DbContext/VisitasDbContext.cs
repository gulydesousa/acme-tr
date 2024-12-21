using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Data;
using VisitasApp.Core.Domain.Entities;

namespace VisitasApp.Infrastructure.Data
{
    public class VisitasDbContext : DbContext, IVisitasDbContext
    {
        public VisitasDbContext(DbContextOptions<VisitasDbContext> options) : base(options) { }

        public DbSet<Visita> Visitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de procedimientos almacenados
            modelBuilder.Entity<Visita>().ToTable("Visitas");
        }
      
    }

}