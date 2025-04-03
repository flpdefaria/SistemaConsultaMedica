using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Controllers;

public class MedicosController : Controller
{
    // GET
    private readonly SisMedContext _context;
    private const int tamanho_pagina = 10;
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
        ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / tamanho_pagina);
        
        return View(medicos.Skip((pagina - 1) * tamanho_pagina).Take(tamanho_pagina));
    }
}