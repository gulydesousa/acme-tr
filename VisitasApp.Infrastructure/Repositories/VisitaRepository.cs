using VisitasApp.Core.Data;
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
    }
}