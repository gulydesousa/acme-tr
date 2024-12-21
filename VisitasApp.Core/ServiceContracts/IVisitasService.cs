using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;

namespace VisitasApp.Core.ServicesContract
{
    public interface IVisitasService
    {
        Task<IEnumerable<Visita>> VisitasGetAllAsync();
        Task<Visita> VisitasGetByIdAsync(int id);
        Task<Visita> VisitasCreateAsync(CreateVisitaDto visita);
        Task VisitasUpdateAsync(Visita visita);
        Task VisitasDeleteAsync(int id);
    }
}