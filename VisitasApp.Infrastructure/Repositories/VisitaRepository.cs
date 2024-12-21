using VisitasApp.Core.Data;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.Domain.RepositoryContracts;
using VisitasApp.Core.DTO;

namespace VisitasApp.Infrastructure.Repositories
{
    public class VisitaRepository : IVisitaRepository
    {
        private readonly IVisitasDbContext _context;

        public VisitaRepository(IVisitasDbContext context)
        {
            _context = context;
        }

        public async Task<Visita> VisitasCreateAsync(CreateVisitaDto visitaDto)
        {
            return await _context.VisitasCreateAsync(visitaDto);
        }

        public async Task<IEnumerable<Visita>> VisitasGetAllAsync()
        {
            return await _context.VisitasGetAllAsync();
        }

        public async Task<Visita?> VisitasGetByIdAsync(int id)
        {
            return await _context.VisitasGetByIdAsync(id);
        }

        public async Task VisitasUpdateAsync(Visita visita)
        {
            await _context.VisitasUpdateAsync(visita);
        }

        public async Task VisitasDeleteAsync(int id)
        {
            await _context.VisitasDeleteAsync(id);
        }
    }
}