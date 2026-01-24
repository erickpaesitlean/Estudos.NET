using Microsoft.EntityFrameworkCore;
using Paciente.Domain.Entities;

namespace Paciente.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<PacienteEntity> Pacientes => Set<PacienteEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PacienteEntity>(entity =>
        {
            entity.ToTable("pacientes");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .ValueGeneratedNever();

            entity.Property(e => e.Nome)
                  .HasColumnName("nome")
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(e => e.Cpf)
                  .HasColumnName("cpf")
                  .HasMaxLength(11)
                  .IsRequired();

            entity.Property(e => e.DataNascimento)
                  .HasColumnName("data_nascimento");
        });
    }
}
