using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Controllers;

public class MedicosController : Controller
{
    // GET
    private readonly SisMedContext _context;

    public MedicosController(SisMedContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var medicos = _context.Medicos.Select(x => new ListarMedicoViewModel
        {
            Id = x.Id,
            CRM = x.CRM,
            Name = x.Name
        });
        
        return View(medicos);
    }
}