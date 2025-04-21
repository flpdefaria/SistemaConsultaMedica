using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.Models.Enums;
using SistemaConsultaMedica.ViewModels.Consultas;

namespace SistemaConsultaMedica.Controllers;

public class ConsultasController : Controller
{
    private readonly SisMedContext _context;
    private readonly IValidator<AdicionarConsultaViewModel> _adicionarConsultaValidator;
    
    private const int tamanhoPagina = 10;

    public ConsultasController(SisMedContext context, IValidator<AdicionarConsultaViewModel> adicionarConsultaValidator)
    {
        _context = context;
        _adicionarConsultaValidator = adicionarConsultaValidator;
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
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(AdicionarConsultaViewModel dados)
    {

        var validacao = _adicionarConsultaValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            ViewBag.TiposConsulta = new[]
            {
                new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() },
            };
        
            ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }

        var consulta = new Consulta
        {
            Data = dados.Data,
            IdPaciente = dados.IdPaciente,
            IdMedico = dados.IdMedico,
            Tipo = dados.Tipo
        };

        _context.Consultas.Add(consulta);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Editar(int id)
    {
        var consulta = _context.Consultas.Include(x => x.Paciente)
            .FirstOrDefault(x => x.Id == id);

        if (consulta != null)
        {
            ViewBag.TiposConsulta = new[]
            {
                new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() },
            };
        
            ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            
            return View(new EditarConsultaViewModel
            {
                IdMedico = consulta.IdMedico,
                IdPaciente = consulta.IdPaciente,
                NamePaciente = consulta.Paciente.Name,
                Data = consulta.Data,
                Tipo = consulta.Tipo
            });
        }

        return NotFound();
    }
}