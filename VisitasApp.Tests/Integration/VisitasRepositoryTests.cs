using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Data;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.Domain.RepositoryContracts;
using VisitasApp.Core.DTO;
using VisitasApp.Infrastructure.Repositories;
using VisitasApp.Tests.TestData;

namespace VisitasApp.Tests.Integration
{
    /// <summary>
    /// Pruebas unitarias de integración para el repositorio de visitas.
    /// Esta clase prueba la funcionalidad del repositorio de visitas en conjunto con la base de datos en memoria.
    /// </summary>
    public class VisitaRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<VisitasDbContextTest> _dbContextOptions;
        private readonly IVisitasDbContext _context;
        private readonly IVisitaRepository _repository;

        public VisitaRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<VisitasDbContextTest>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new VisitasDbContextTest(_dbContextOptions);
            _repository = new VisitaRepository(_context);

            // Seed the database with initial data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var visitas = new List<Visita>
            {
                new Visita { Id = 1, NombreCliente = "Cliente Test", FechaVisita = DateTime.Now, NombreVendedor = "Vendedor Test", Notas = "Notas de prueba" },
                new Visita { Id = 2, NombreCliente = "Cliente Test 2", FechaVisita = DateTime.Now, NombreVendedor = "Vendedor Test 2", Notas = "Notas de prueba 2" }
            };

            _context.Visitas.AddRange(visitas);
            _context.SaveChangesAsync().Wait();
        }

        [Fact]
        public async Task VisitasCreateAsync_AddsVisitaToDatabase()
        {
            // Arrange
            var visitaDto = new CreateVisitaDto { NombreCliente = "Nuevo Cliente", FechaVisita = DateTime.Now, NombreVendedor = "Nuevo Vendedor", Notas = "Nuevas Notas" };

            // Act
            var result = await _repository.VisitasCreateAsync(visitaDto);

            // Assert
            var addedVisita = await _context.Visitas.FindAsync(result.Id);
            Assert.NotNull(addedVisita);
            Assert.Equal("Nuevo Cliente", addedVisita.NombreCliente);
        }

        [Fact]
        public async Task VisitasGetAllAsync_ReturnsAllVisitas()
        {
            // Act
            var result = await _repository.VisitasGetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task VisitasGetByIdAsync_ReturnsCorrectVisita()
        {
            // Act
            var result = await _repository.VisitasGetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cliente Test", result.NombreCliente);
        }

        [Fact]
        public async Task VisitasUpdateAsync_UpdatesVisitaInDatabase()
        {
            // Arrange
            var visita = await _repository.VisitasGetByIdAsync(1);
            visita!.NombreCliente = "Cliente Actualizado";

            // Act
            await _repository.VisitasUpdateAsync(visita);

            // Assert
            var updatedVisita = await _context.Visitas.FindAsync(1);
            Assert.Equal("Cliente Actualizado", updatedVisita!.NombreCliente);
        }

        [Fact]
        public async Task VisitasDeleteAsync_DeletesVisitaFromDatabase()
        {
            // Act
            await _repository.VisitasDeleteAsync(1);

            // Assert
            var deletedVisita = await _context.Visitas.FindAsync(1);
            Assert.Null(deletedVisita);
        }

        public void Dispose()
        {
            var testContext = (VisitasDbContextTest)_context;
            testContext.Database.EnsureDeleted();
            testContext.Dispose();
        }
    }
}
