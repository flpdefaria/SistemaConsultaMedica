using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Controllers;

public class MedicosController : Controller
{
    // GET
    private readonly SisMedContext _context;
    private const int tamanhoPagina = 10;
    public MedicosController(SisMedContext context)
    {
        _context = context;
    }
    public IActionResult Index(string filtro, int pagina = 1)
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
        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / tamanhoPagina);
        
        return View(medicos.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina));
    }

    public IActionResult Adicionar()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(AdicionarMedicoViewModel dados)
    {
        if (!ModelState.IsValid)
        {
            return View(dados);
        }
        
        var medico = new Medico
        {
            CRM = dados.CRM,
            Name = dados.Name
        };
        
        _context.Medicos.Add(medico);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
}