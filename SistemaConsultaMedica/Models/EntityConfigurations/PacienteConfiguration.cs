using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaConsultaMedica.Models.Entities;

namespace SistemaConsultaMedica.Models.EntityConfigurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CPF)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(x => x.CPF)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.DataNascimento)
            .IsRequired();

        builder.HasOne(p => p.InformacoesComplementares)
            .WithOne(i => i.Paciente)
            .HasForeignKey<InformacoesComplementaresPaciente>(i => i.IdPaciente);
    }   
}