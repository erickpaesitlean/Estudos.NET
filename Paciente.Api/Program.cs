using Microsoft.EntityFrameworkCore;
using Paciente.Application.Services;
using Paciente.Domain.Interfaces;
using Paciente.Infrastructure.Data;
using Paciente.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 DbContext (InMemory)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// 🔹 DI
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<PacienteService>();

var app = builder.Build();

// 🔹 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
