namespace Paciente.Application.DTOs;

public record CreatePacienteDto(
    string Nome,
    DateTime DataNascimento,
    string CPF
);
