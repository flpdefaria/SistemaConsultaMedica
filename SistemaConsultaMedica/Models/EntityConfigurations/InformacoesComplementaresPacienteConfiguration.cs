using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaConsultaMedica.Models.Entities;

namespace SistemaConsultaMedica.Models.EntityConfigurations;

public class InformacoesComplementaresPacienteConfiguration : IEntityTypeConfiguration<InformacoesComplementaresPaciente>
{
    public void Configure(EntityTypeBuilder<InformacoesComplementaresPaciente> builder)
    {
        builder.ToTable("InformacoesComplementaresPacientes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Alergias)
            .HasMaxLength(200);

        builder.Property(x => x.MedicamentosEmUso)
            .HasMaxLength(200);

        builder.Property(x => x.CirurgiasRealizadas)
            .HasMaxLength(200);
    }
}