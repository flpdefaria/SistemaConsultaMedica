using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.ViewModels.Pacientes;

namespace SistemaConsultaMedica.Controllers;

public class PacientesController : Controller
{
    //GET
    private readonly SisMedContext _context;

    public PacientesController(SisMedContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var pacientes = _context.Pacientes
            .Select(x => new ListarPacientesViewModel
            {
                Id = x.Id,
                CPF = x.CPF,
                Name = x.Name,
                DataNascimento = x.DataNascimento
            });
        
        return View(pacientes);
    }
}