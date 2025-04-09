using System.ComponentModel.DataAnnotations;

namespace SistemaConsultaMedica.ViewModels.Pacientes;

public class AdicionarPacienteViewModel
{
    public string CPF { get; set; } = string.Empty;
    
    [Display(Name="Nome")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;
    
    [Display(Name="Data de Nascimento")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }
}