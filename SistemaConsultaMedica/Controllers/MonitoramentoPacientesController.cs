using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.MonitoramentoPaciente;

namespace SistemaConsultaMedica.Controllers;

[Route("Monitoramento")]
    
public class MonitoramentoPacientesController : Controller
{
    private readonly SisMedContext _context;
    
    public MonitoramentoPacientesController(SisMedContext context)
    {
        _context = context;
    }
    
    public IActionResult Index(int idPaciente)
    {
        var monitoramentos = _context.MonitoriamentoPaciente
            .Where(x => x.IdPaciente == idPaciente)
            .Select(x => new ListarMonitoramentoViewModel
            {
                Id = x.Id,
                PressaoArterial = x.PressaoArterial,
                SaturacaoOxigenio = x.SaturacaoOxigenio,
                Temperatura = x.Temperatura,
                FrequenciaCardiaca = x.FrequenciaCardiaca,
                DataAfericao = x.DataAfericao
            });
        
        return View(monitoramentos);
    }
}