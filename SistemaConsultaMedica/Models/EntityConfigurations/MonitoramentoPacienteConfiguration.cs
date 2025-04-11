using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaConsultaMedica.Models.Entities;

namespace SistemaConsultaMedica.Models.EntityConfigurations;

public class MonitoramentoPacienteConfiguration : IEntityTypeConfiguration<MonitoriamentoPaciente>
{
    public void Configure(EntityTypeBuilder<MonitoriamentoPaciente> builder)
    {
        builder.ToTable("MonitoramentoPaciente");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PressaoArterial)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Temperatura)
            .HasColumnType("decimal(3,1)");

        builder.Property(x => x.SaturacaoOxigenio)
            .HasColumnType("TINYINT");
        
        builder.Property(x => x.FrequenciaCardiaca)
            .HasColumnType("TINYINT");
        
        builder.Property(x => x.DataAfericao)
            .IsRequired();
    }
}