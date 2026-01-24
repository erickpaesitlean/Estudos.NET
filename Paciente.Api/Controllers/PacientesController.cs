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
}
