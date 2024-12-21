using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitasApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFiles = Directory.GetFiles(Path.Combine("..", "VisitasApp.Infrastructure", "TSqlScripts", "Visitas"), "*.sql");

            foreach (var file in sqlFiles)
            {
                var sql = File.ReadAllText(file);
                migrationBuilder.Sql(sql);
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Si tienes scripts para eliminar los procedimientos almacenados, puedes agregarlos aquí
            // Por ejemplo, podrías tener archivos .sql con DROP PROCEDURE statements
            var sqlFiles = Directory.GetFiles(Path.Combine("..", "VisitasApp.Infrastructure", "TSqlScripts", "Visitas", "Drop"), "*.sql");

            foreach (var file in sqlFiles)
            {
                var sql = File.ReadAllText(file);
                migrationBuilder.Sql(sql);
            }
        }
    }
}
