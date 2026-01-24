using Paciente.Domain.Entities;

namespace Paciente.Domain.Interfaces;

public interface IPacienteRepository
{
    Task AddAsync(PacienteEntity paciente);
    Task<PacienteEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<PacienteEntity>> GetAllAsync();
    Task UpdateAsync(PacienteEntity paciente);
    Task DeleteAsync(Guid id);
}
