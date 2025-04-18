﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaConsultaMedica.Models.Contexts;

#nullable disable

namespace SistemaConsultaMedica.Migrations
{
    [DbContext(typeof(SisMedContext))]
    partial class SisMedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdMedico")
                        .HasColumnType("int");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMedico");

                    b.HasIndex("IdPaciente");

                    b.ToTable("Consultas", (string)null);
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alergias")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CirurgiasRealizadas")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<string>("MedicamentosEmUso")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente")
                        .IsUnique();

                    b.ToTable("InformacoesComplementaresPacientes", (string)null);
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CRM")
                        .IsUnique();

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.MonitoriamentoPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAfericao")
                        .HasColumnType("datetime2");

                    b.Property<byte>("FrequenciaCardiaca")
                        .HasColumnType("TINYINT");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<string>("PressaoArterial")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<byte>("SaturacaoOxigenio")
                        .HasColumnType("TINYINT");

                    b.Property<decimal>("Temperatura")
                        .HasColumnType("decimal(3,1)");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente");

                    b.ToTable("MonitoramentoPaciente", (string)null);
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Consulta", b =>
                {
                    b.HasOne("SistemaConsultaMedica.Models.Entities.Medico", "Medico")
                        .WithMany("Consultas")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaConsultaMedica.Models.Entities.Paciente", "Paciente")
                        .WithMany("Consultas")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.HasOne("SistemaConsultaMedica.Models.Entities.Paciente", "Paciente")
                        .WithOne("InformacoesComplementares")
                        .HasForeignKey("SistemaConsultaMedica.Models.Entities.InformacoesComplementaresPaciente", "IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.MonitoriamentoPaciente", b =>
                {
                    b.HasOne("SistemaConsultaMedica.Models.Entities.Paciente", "Paciente")
                        .WithMany("Monitoramentos")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Medico", b =>
                {
                    b.Navigation("Consultas");
                });

            modelBuilder.Entity("SistemaConsultaMedica.Models.Entities.Paciente", b =>
                {
                    b.Navigation("Consultas");

                    b.Navigation("InformacoesComplementares");

                    b.Navigation("Monitoramentos");
                });
#pragma warning restore 612, 618
        }
    }
}
