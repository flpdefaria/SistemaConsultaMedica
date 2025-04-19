using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Enums;
using SistemaConsultaMedica.ViewModels.Consultas;

namespace SistemaConsultaMedica.Controllers;

public class ConsultasController : Controller
{
    private readonly SisMedContext _context;
    private const int tamanhoPagina = 10;

    public ConsultasController(SisMedContext context)
    {
        _context = context;
    }

    public IActionResult Index(string filtro, int pagina = 1)
    {
        var consultas = _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Where(c => c.Paciente.Name.Contains(filtro) || c.Medico.Name.Contains(filtro))
            .Select(c => new ListarConsultaViewModel
            {
                Id = c.Id,
                Paciente = c.Paciente.Name,
                Medico = c.Medico.Name,
                Data = c.Data,
                Tipo = c.Tipo == TipoConsulta.Eletiva ? "Eletiva" : "Urgencia"
            });

        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)consultas.Count() / tamanhoPagina);
        
        return View(consultas.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina));
    }

    public IActionResult Adicionar()
    {

        ViewBag.TiposConsulta = new[]
        {
            new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
            new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() },
        };
        
        ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        return View();
    }
}