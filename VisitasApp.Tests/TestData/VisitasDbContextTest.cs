using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Data;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;

namespace VisitasApp.Tests.TestData
{

    /// <summary>
    /// Proporciona una capa de abstracción para interactuar con la base de datos en pruebas unitarias, permitiendo realizar operaciones CRUD en la tabla Visitas.
    /// </summary>
    public class VisitasDbContextTest : DbContext, IVisitasDbContext
    {
        public VisitasDbContextTest(DbContextOptions<VisitasDbContextTest> options) : base(options) { }

        public DbSet<Visita> Visitas { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public Task<Visita> VisitasCreateAsync(CreateVisitaDto visitaDto)
        {
            var visita = new Visita
            {
                Id = Visitas.Any() ? Visitas.Max(v => v.Id) + 1 : 1,
                NombreCliente = visitaDto.NombreCliente,
                FechaVisita = visitaDto.FechaVisita,
                NombreVendedor = visitaDto.NombreVendedor,
                Notas = visitaDto.Notas
            };

            Visitas.Add(visita);
            SaveChanges();

            return Task.FromResult(visita);
        }

        public async Task<IEnumerable<Visita>> VisitasGetAllAsync()
        {
            return await Visitas.ToListAsync();
        }

        public Task<Visita?> VisitasGetByIdAsync(int id)
        {
            return Visitas.FindAsync(id).AsTask();
        }

        public Task VisitasUpdateAsync(Visita visita)
        {
            Visitas.Update(visita);
            return SaveChangesAsync();
        }

        public Task VisitasDeleteAsync(int id)
        {
            var visita = Visitas.Find(id);
            if (visita != null)
            {
                Visitas.Remove(visita);
                SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
