using VisitasApp.Core.Domain.Entities;

namespace VisitasApp.Core.ServicesContract
{
    public interface IVisitasService
    {
        Task<IEnumerable<Visita>> VisitasGetAllAsync();
        Task<Visita> VisitasGetByIdAsync(int id);
        Task<Visita> VisitasCreateAsync(Visita visita);
        Task VisitasUpdateAsync(Visita visita);
        Task VisitasDeleteAsync(int id);
    }
}