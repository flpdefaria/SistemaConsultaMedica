namespace SistemaConsultaMedica.ViewModels.Pacientes;

public class AdicionarPacienteViewModel
{
    public string CPF { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public DateTime DataNascimento { get; set; }
}