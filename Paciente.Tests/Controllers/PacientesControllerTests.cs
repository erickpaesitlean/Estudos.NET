using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paciente.Api.Controllers;
using Paciente.Application.DTOs;
using Paciente.Application.Services;
using Paciente.Domain.Entities;
using Paciente.Domain.Interfaces;
using Xunit;

namespace Paciente.Tests.Controllers
{
    public class PacientesControllerTests
    {
        [Fact]
        public async Task Criar_ReturnsCreated()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var dto = new CreatePacienteDto("Joao", DateTime.UtcNow.AddYears(-30), "12345678901");

            var result = await controller.Criar(dto);

            Assert.IsType<CreatedResult>(result);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<PacienteEntity>()), Times.Once);
        }

        [Fact]
        public async Task Listar_ReturnsOkWithPacientes()
        {
            var pacientes = new List<PacienteEntity>
            {
                new PacienteEntity("Ana", DateTime.UtcNow.AddYears(-25), "11111111111"),
                new PacienteEntity("Carlos", DateTime.UtcNow.AddYears(-40), "22222222222")
            };

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(pacientes);

            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var result = await controller.Listar();

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsAssignableFrom<IEnumerable<PacienteEntity>>(ok.Value);
            Assert.Equal(2, System.Linq.Enumerable.Count(returned));
        }

        [Fact]
        public async Task ObterPorId_ReturnsOk_WhenFound()
        {
            var paciente = new PacienteEntity("Davi", DateTime.UtcNow.AddYears(-30), "55555555555");

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(paciente);

            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var result = await controller.ObterPorId(Guid.NewGuid());

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<PacienteEntity>(ok.Value);
            Assert.Equal(paciente.Id, returned.Id);
        }

        [Fact]
        public async Task ObterPorId_ReturnsNotFound_WhenMissing()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PacienteEntity?)null);

            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var result = await controller.ObterPorId(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Excluir_ReturnsNoContent_WhenDeleted()
        {
            var paciente = new PacienteEntity("Eve", DateTime.UtcNow.AddYears(-50), "66666666666");

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(paciente);
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var result = await controller.Excluir(Guid.NewGuid());

            Assert.IsType<NoContentResult>(result);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Excluir_ReturnsNotFound_WhenMissing()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PacienteEntity?)null);

            var service = new PacienteService(mockRepo.Object);
            var controller = new PacientesController(service);

            var result = await controller.Excluir(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
