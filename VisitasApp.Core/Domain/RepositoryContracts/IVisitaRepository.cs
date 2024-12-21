using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;

namespace VisitasApp.Core.Domain.RepositoryContracts
{
    public interface IVisitaRepository
    {
        #region CRUD Visitas
        Task<Visita> VisitasCreateAsync(CreateVisitaDto visitaDto);
        Task<IEnumerable<Visita>> VisitasGetAllAsync();
        Task<Visita?> VisitasGetByIdAsync(int id);
        Task VisitasUpdateAsync(Visita visita);
        Task VisitasDeleteAsync(int id);
        #endregion
    }
}
