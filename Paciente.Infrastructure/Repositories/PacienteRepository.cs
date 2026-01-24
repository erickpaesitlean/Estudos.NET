using Microsoft.EntityFrameworkCore;
using Paciente.Domain.Entities;
using Paciente.Domain.Interfaces;
using Paciente.Infrastructure.Data;

namespace Paciente.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly AppDbContext _context;

    public PacienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PacienteEntity paciente)
    {
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PacienteEntity>> GetAllAsync()
        => await _context.Pacientes.ToListAsync();

    public async Task<PacienteEntity?> GetByIdAsync(Guid id)
        => await _context.Pacientes.FindAsync(id);

    public async Task UpdateAsync(PacienteEntity paciente)
    {
        _context.Pacientes.Update(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var paciente = await GetByIdAsync(id);
        if (paciente is null) return;

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
    }
}
