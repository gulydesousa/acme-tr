using Microsoft.AspNetCore.Mvc;
using Moq;
using VisitasApp.Api.Controllers;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.Domain.RepositoryContracts;
using VisitasApp.Core.DTO;

namespace VisitasApp.Tests.Controllers
{
    public class VisitasControllerTests
    {
        private readonly Mock<IVisitaRepository> _mockRepo;
        private readonly VisitasController _controller;

        public VisitasControllerTests()
        {
            _mockRepo = new Mock<IVisitaRepository>();
            _controller = new VisitasController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddVisita_ReturnsCreatedAtActionResult_WhenVisitaIsValid()
        {
            // Arrange
            var visitaDto = new CreateVisitaDto
            {
                NombreCliente = "Cliente Test",
                FechaVisita = DateTime.Now,
                NombreVendedor = "Vendedor Test",
                Notas = "Notas Test"
            };

            var visitaEntity = new Visita
            {
                Id = 1, // Simulate setting the ID after adding to the database
                NombreCliente = visitaDto.NombreCliente,
                FechaVisita = visitaDto.FechaVisita,
                NombreVendedor = visitaDto.NombreVendedor,
                Notas = visitaDto.Notas
            };

            _mockRepo.Setup(repo => repo.VisitasCreateAsync(It.IsAny<CreateVisitaDto>())).ReturnsAsync(visitaEntity);

            // Act
            var result = await _controller.AddVisita(visitaDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Visita>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue?.Id);
        }


        [Fact]
        public async Task GetVisitas_ReturnsOkObjectResult_WhenVisitasExist()
        {
            // Arrange
            var visitas = new List<Visita>
            {
                new Visita
                {
                    Id = 1,
                    NombreCliente = "Cliente Test",
                    FechaVisita = System.DateTime.Now,
                    NombreVendedor = "Vendedor Test",
                    Notas = "Notas de prueba"
                },
                new Visita
                {
                    Id = 2,
                    NombreCliente = "Cliente Test 2",
                    FechaVisita = System.DateTime.Now,
                    NombreVendedor = "Vendedor Test 2",
                    Notas = "Notas de prueba 2"
                }
            };

            _mockRepo.Setup(repo => repo.VisitasGetAllAsync()).ReturnsAsync(visitas);

            // Act
            var result = await _controller.GetVisitas();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Visita>>(okObjectResult.Value);
            Assert.Equal(visitas.Count, returnValue.Count);
        }

        [Fact]
        public async Task GetVisita_ReturnsOkObjectResult_WhenVisitaExists()
        {
            // Arrange
            var visita = new Visita
            {
                Id = 1,
                NombreCliente = "Cliente Test",
                FechaVisita = System.DateTime.Now,
                NombreVendedor = "Vendedor Test",
                Notas = "Notas de prueba"
            };

            _mockRepo.Setup(repo => repo.VisitasGetByIdAsync(It.IsAny<int>())).ReturnsAsync(visita);

            // Act
            var result = await _controller.GetVisita(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Visita>(okObjectResult.Value);
            Assert.Equal(visita.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetVisita_ReturnsNotFoundResult_WhenVisitaDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.VisitasGetByIdAsync(It.IsAny<int>())).ReturnsAsync((Visita?)null);

            // Act
            var result = await _controller.GetVisita(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateVisita_ReturnsNoContentResult_WhenVisitaIsValid()
        {
            // Arrange
            var visita = new Visita
            {
                Id = 1,
                NombreCliente = "Cliente Test",
                FechaVisita = System.DateTime.Now,
                NombreVendedor = "Vendedor Test",
                Notas = "Notas de prueba"
            };

            _mockRepo.Setup(repo => repo.VisitasUpdateAsync(It.IsAny<Visita>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateVisita(1, visita);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteVisita_ReturnsNoContentResult_WhenVisitaExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.VisitasDeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteVisita(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteVisita_ReturnsNoContentResult_WhenVisitaDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.VisitasDeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteVisita(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteVisita_ReturnsNoContentResult_WhenVisitaIsNull()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.VisitasDeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteVisita(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateVisita_ReturnsBadRequestResult_WhenIdDoesNotMatch()
        {
            // Arrange
            var visita = new Visita
            {
                Id = 2,
                NombreCliente = "Cliente Test",
                FechaVisita = System.DateTime.Now,
                NombreVendedor = "Vendedor Test",
                Notas = "Notas de prueba"
            };

            // Act
            var result = await _controller.UpdateVisita(1, visita);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateVisita_ReturnsNoContentResult_WhenIdMatches()
        {
            // Arrange
            var visita = new Visita
            {
                Id = 1,
                NombreCliente = "Cliente Test",
                FechaVisita = System.DateTime.Now,
                NombreVendedor = "Vendedor Test",
                Notas = "Notas de prueba"
            };

            _mockRepo.Setup(repo => repo.VisitasUpdateAsync(It.IsAny<Visita>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateVisita(1, visita);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}