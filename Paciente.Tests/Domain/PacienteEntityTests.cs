using System;
using Xunit;
using Paciente.Domain.Entities;

namespace Paciente.Tests.Domain
{
    public class PacienteEntityTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var nome = "Teste";
            var data = new DateTime(1990,1,1);
            var cpf = "99999999999";

            var paciente = new PacienteEntity(nome, data, cpf);

            Assert.Equal(nome, paciente.Nome);
            Assert.Equal(data, paciente.DataNascimento);
            Assert.Equal(cpf, paciente.Cpf);
            Assert.NotEqual(Guid.Empty, paciente.Id);
        }
    }
}
