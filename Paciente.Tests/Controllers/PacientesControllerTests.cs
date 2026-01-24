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
    }
}
