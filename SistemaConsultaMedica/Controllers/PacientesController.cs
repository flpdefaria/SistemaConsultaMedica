using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.ViewModels.Pacientes;

namespace SistemaConsultaMedica.Controllers;

public class PacientesController : Controller
{
    private readonly SisMedContext _context;
    private readonly IValidator<AdicionarPacienteViewModel> _adicionarPacienteValidator;
    private const int tamanhoPagina = 10;
    
    public PacientesController(SisMedContext context, IValidator<AdicionarPacienteViewModel> adicionarPacienteValidator)
    {
        _context = context;
        _adicionarPacienteValidator = adicionarPacienteValidator;
    }
    
    public IActionResult Index(string filtro, int pagina = 1)
    {
        var pacientes = _context.Pacientes
            .Where(x => x.Name.Contains(filtro) || x.CPF.Contains(filtro))
            .Select(x => new ListarPacienteViewModel
            {
                Id = x.Id,
                CPF = x.CPF,
                Name = x.Name
            });
        
        ViewBag.Filtro = filtro;
        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)pacientes.Count() / tamanhoPagina);
        return View(pacientes.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina));
    }
    
    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(AdicionarPacienteViewModel dados)
    {
        var validacao = _adicionarPacienteValidator.Validate(dados); 

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var paciente = new Paciente
        {
            CPF = Regex.Replace(dados.CPF, "[^0-9]", ""),
            Name = dados.Name,
            DataNascimento = dados.DataNascimento
        };
        
        _context.Pacientes.Add(paciente);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
}