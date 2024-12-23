using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitasApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialVisitasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Datos de prueba
            migrationBuilder.Sql(@"
            INSERT INTO visitas (NombreCliente, FechaVisita, NombreVendedor, Notas) VALUES ('Cliente1', '2023-10-01 10:00:00', 'Vendedor1', 'Nota1');
            INSERT INTO visitas (NombreCliente, FechaVisita, NombreVendedor, Notas) VALUES ('Cliente2', '2023-10-02 11:30:00', 'Vendedor2', '');
            INSERT INTO visitas (NombreCliente, FechaVisita, NombreVendedor, Notas) VALUES ('Cliente3', '2023-10-03 14:45:00', 'Vendedor3', 'Nota3');
            INSERT INTO visitas (NombreCliente, FechaVisita, NombreVendedor, Notas) VALUES ('Cliente4', '2023-10-04 09:15:00', 'Vendedor4', '');
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally, remove the data if the migration is rolled back
            migrationBuilder.Sql(@"
            DELETE FROM visitas WHERE NombreCliente IN ('Cliente1', 'Cliente2', 'Cliente3', 'Cliente4');
        ");

        }
    }
}
