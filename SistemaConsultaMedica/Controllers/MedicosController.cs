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
    public IActionResult Index(string filtro)
    {
        var medicos = _context.Medicos
            .Where(x => x.Name.Contains(filtro) || x.CRM.Contains(filtro))
            .Select(x => new ListarMedicoViewModel
        {
            Id = x.Id,
            CRM = x.CRM,
            Name = x.Name
        });
        
        ViewBag.Filtro = filtro;
        
        return View(medicos);
    }
}