using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SistemaConsultaMedica.Models.Enums;

namespace SistemaConsultaMedica.ViewModels.Consultas;

public class AdicionarConsultaViewModel
{
    [DataType(DataType.Date)]
    public DateTime Data { get; set; }
    
    public TipoConsulta Tipo { get; set; }
    
    [Display(Name = "Paciente")]
    public int IdPaciente { get; set; }
    
    [Display(Name = "Médico")]
    public int IdMedico { get; set; }
}