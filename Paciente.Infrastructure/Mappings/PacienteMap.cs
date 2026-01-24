using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paciente.Domain.Entities;

public class PacienteMap : IEntityTypeConfiguration<PacienteEntity>
{
    public void Configure(EntityTypeBuilder<PacienteEntity> builder)
    {
        builder.ToTable("pacientes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Cpf)
            .HasColumnName("cpf")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(p => p.DataNascimento)
            .HasColumnName("data_nascimento")
            .IsRequired();
    }
}
