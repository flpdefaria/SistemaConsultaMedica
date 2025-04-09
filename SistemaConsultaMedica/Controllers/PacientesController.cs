using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.ViewModels.Pacientes;

namespace SistemaConsultaMedica.Controllers;

public class PacientesController : Controller
{
    private readonly SisMedContext _context;
    private const int tamanhoPagina = 10;
    
    public PacientesController(SisMedContext context)
    {
        _context = context;
    }
    
    public IActionResult Index(string filtro, int pagina = 1)
    {
        var pacientes = _context.Pacientes
            .Where(x => x.Name.Contains(filtro) || x.CPF.Contains(filtro))
            .Select(x => new ListarPacienteViewModel
            {
                Id = x.Id,
                CPF = x.CPF,
                Name = x.Name,
            });
        
        ViewBag.Filtro = filtro;
        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)pacientes.Count() / tamanhoPagina);
        return View(pacientes.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina));
    }
}