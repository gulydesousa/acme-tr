using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VisitasApp.Core.Data;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;

namespace VisitasApp.Infrastructure.Data
{
    public partial class VisitasDbContext : DbContext, IVisitasDbContext
    {
        public VisitasDbContext(DbContextOptions<VisitasDbContext> options) : base(options) { }

        public DbSet<Visita> Visitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de procedimientos almacenados
            modelBuilder.Entity<Visita>().ToTable("Visitas");
        }


        #region CRUD Visitas
        public async Task<Visita> VisitasCreateAsync(CreateVisitaDto visitaDto)
        {
            using (var connection = Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Visitas_Create";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NombreCliente", visitaDto.NombreCliente));
                    command.Parameters.Add(new SqlParameter("@FechaVisita", visitaDto.FechaVisita));
                    command.Parameters.Add(new SqlParameter("@NombreVendedor", visitaDto.NombreVendedor));
                    command.Parameters.Add(new SqlParameter("@Notas", visitaDto.Notas));

                    var idParam = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(idParam);

                    await command.ExecuteNonQueryAsync();

                    var visita = new Visita
                    {
                        Id = (int)idParam.Value,
                        NombreCliente = visitaDto.NombreCliente,
                        FechaVisita = visitaDto.FechaVisita,
                        NombreVendedor = visitaDto.NombreVendedor,
                        Notas = visitaDto.Notas
                    };

                    return visita;
                }
            }
        }

        public async Task<IEnumerable<Visita>> VisitasGetAllAsync()
        {
            return await Visitas.FromSqlRaw("EXEC Visitas_GetAll").ToListAsync();
        }

        public async Task<Visita?> VisitasGetByIdAsync(int id)
        {
            using (var connection = Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Visitas_GetById";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var visita = new Visita
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                FechaVisita = reader.GetDateTime(reader.GetOrdinal("FechaVisita")),
                                NombreVendedor = reader.GetString(reader.GetOrdinal("NombreVendedor")),
                                Notas = reader.GetString(reader.GetOrdinal("Notas"))
                            };

                            return visita;
                        }
                    }
                }
            }

            return null;
        }

        public async Task VisitasUpdateAsync(Visita visita)
        {
            using (var connection = Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Visitas_Update";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", visita.Id));
                    command.Parameters.Add(new SqlParameter("@NombreCliente", visita.NombreCliente));
                    command.Parameters.Add(new SqlParameter("@FechaVisita", visita.FechaVisita));
                    command.Parameters.Add(new SqlParameter("@NombreVendedor", visita.NombreVendedor));
                    command.Parameters.Add(new SqlParameter("@Notas", visita.Notas));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task VisitasDeleteAsync(int id)
        {
            using (var connection = Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Visitas_Delete";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", id));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        #endregion
    }
}