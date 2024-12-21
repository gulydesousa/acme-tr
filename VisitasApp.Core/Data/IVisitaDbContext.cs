using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;

namespace VisitasApp.Core.Data
{
    public interface IVisitasDbContext
    {
        DbSet<Visita> Visitas { get; set; }         
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #region CRUD Visitas
        Task<Visita> VisitasCreateAsync(CreateVisitaDto visitaDto);
        Task<IEnumerable<Visita>> VisitasGetAllAsync();
        Task<Visita?> VisitasGetByIdAsync(int id);
        Task VisitasUpdateAsync(Visita visita);
        Task VisitasDeleteAsync(int id);
        #endregion
    }
}