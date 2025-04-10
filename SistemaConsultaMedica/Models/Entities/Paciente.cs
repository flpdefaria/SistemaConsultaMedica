namespace SistemaConsultaMedica.Models.Entities;

public class Paciente
{
    public int Id { get; set; }

    public string CPF { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public DateTime DataNascimento { get; set; }

    public InformacoesComplementaresPaciente? InformacoesComplementares { get; set; }
}