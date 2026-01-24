using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Paciente.Application.DTOs;
using Paciente.Application.Services;
using Paciente.Domain.Entities;
using Paciente.Domain.Interfaces;
using Xunit;

namespace Paciente.Tests.Services
{
    public class PacienteServiceTests
    {
        [Fact]
        public async Task CriarAsync_CallsRepositoryAdd()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            var service = new PacienteService(mockRepo.Object);

            var dto = new CreatePacienteDto("Maria", DateTime.UtcNow.AddYears(-20), "33333333333");

            await service.CriarAsync(dto);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<PacienteEntity>()), Times.Once);
        }

        [Fact]
        public async Task ListarAsync_ReturnsAllFromRepository()
        {
            var pacientes = new List<PacienteEntity>
            {
                new PacienteEntity("A", DateTime.UtcNow.AddYears(-10), "44444444444")
            };

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(pacientes);

            var service = new PacienteService(mockRepo.Object);

            var result = await service.ListarAsync();

            Assert.Equal(pacientes, result);
        }

        [Fact]
        public async Task ObterPorIdAsync_ReturnsPaciente_WhenExists()
        {
            var paciente = new PacienteEntity("Bia", DateTime.UtcNow.AddYears(-33), "77777777777");

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(paciente);

            var service = new PacienteService(mockRepo.Object);

            var result = await service.ObterPorIdAsync(Guid.NewGuid());

            Assert.Equal(paciente, result);
        }

        [Fact]
        public async Task ObterPorIdAsync_ReturnsNull_WhenNotFound()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PacienteEntity?)null);

            var service = new PacienteService(mockRepo.Object);

            var result = await service.ObterPorIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task ExcluirAsync_DeletesAndReturnsTrue_WhenExists()
        {
            var paciente = new PacienteEntity("Cleo", DateTime.UtcNow.AddYears(-44), "88888888888");

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(paciente);
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            var service = new PacienteService(mockRepo.Object);

            var result = await service.ExcluirAsync(Guid.NewGuid());

            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task ExcluirAsync_ReturnsFalse_WhenNotFound()
        {
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PacienteEntity?)null);

            var service = new PacienteService(mockRepo.Object);

            var result = await service.ExcluirAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}
