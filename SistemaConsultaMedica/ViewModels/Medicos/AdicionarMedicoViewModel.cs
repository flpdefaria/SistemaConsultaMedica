using System.ComponentModel.DataAnnotations;

namespace SistemaConsultaMedica.ViewModels.Medicos;

public class AdicionarMedicoViewModel
{
    public string CRM { get; set; } = string.Empty;
    
    [Display(Name = "Nome")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;
}