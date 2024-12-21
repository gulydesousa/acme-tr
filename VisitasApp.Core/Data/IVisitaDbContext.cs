using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Domain.Entities;

namespace VisitasApp.Core.Data
{
    public interface IVisitasDbContext
    {
        DbSet<Visita> Visitas { get; set; }
    }
}