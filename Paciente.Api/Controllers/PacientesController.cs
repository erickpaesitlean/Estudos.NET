using Microsoft.AspNetCore.Mvc;
using Paciente.Application.DTOs;
using Paciente.Application.Services;

namespace Paciente.Api.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacientesController : ControllerBase
{
    private readonly PacienteService _service;

    public PacientesController(PacienteService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CreatePacienteDto dto)
    {
        await _service.CriarAsync(dto);
        return Created("", null);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var pacientes = await _service.ListarAsync();
        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var paciente = await _service.ObterPorIdAsync(id);
        if (paciente is null) return NotFound();
        return Ok(paciente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var removed = await _service.ExcluirAsync(id);
        if (!removed) return NotFound();
        return NoContent();
    }
}
