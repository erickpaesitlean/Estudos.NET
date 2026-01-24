using Paciente.Application.DTOs;
using Paciente.Domain.Entities;
using Paciente.Domain.Interfaces;

namespace Paciente.Application.Services;

public class PacienteService
{
    private readonly IPacienteRepository _repository;

    public PacienteService(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task CriarAsync(CreatePacienteDto dto)
    {
        var paciente = new PacienteEntity(
            dto.Nome,
            dto.DataNascimento,
            dto.CPF
        );

        await _repository.AddAsync(paciente);
    }

    public Task<IEnumerable<PacienteEntity>> ListarAsync()
        => _repository.GetAllAsync();
}
