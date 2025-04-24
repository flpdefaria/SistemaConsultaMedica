using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Controllers;

public class MedicosController : Controller
{
    // GET
    private readonly SisMedContext _context;
    private readonly IValidator<AdicionarMedicoViewModel> _adicionarMedicoValidator;
    private readonly IValidator<EditarMedicoViewModel> _editarMedicoValidator;
    private const int tamanhoPagina = 10;

    public MedicosController(SisMedContext context, IValidator<AdicionarMedicoViewModel> adicionarMedicoValidator, IValidator<EditarMedicoViewModel> editarMedicoValidator)
    {
        _context = context;
        _adicionarMedicoValidator = adicionarMedicoValidator;
        _editarMedicoValidator = editarMedicoValidator;
    }

    public IActionResult Index(string filtro, int pagina = 1)
    {
        var query = _context.Medicos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            query = query.OrderBy(x => x.Name).Where(x => x.Name.Contains(filtro) || x.CRM.Contains(filtro));
        }

        var medicos = query
            .Select(x => new ListarMedicoViewModel
            {
                Id = x.Id,
                CRM = x.CRM,
                Name = x.Name
            });

        ViewBag.Filtro = filtro;
        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / tamanhoPagina);

        var medicosPaginados = medicos
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();

        return View(medicosPaginados);
    }


    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(AdicionarMedicoViewModel dados)
    {
        var validacao = _adicionarMedicoValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
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

    public IActionResult Editar(int id)
    {
        var medico = _context.Medicos.Find(id);
        
        if (medico != null)
        {
            return View(new EditarMedicoViewModel
            {
                Id = medico.Id,
                CRM = medico.CRM,
                Name = medico.Name
            });
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, EditarMedicoViewModel dados)
    {
        var validacao = _editarMedicoValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var medico = _context.Medicos.Find(id);

        if (medico != null)
        {
            medico.CRM = dados.CRM;
            medico.Name = dados.Name;

            _context.Medicos.Update(medico);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        return NotFound();
    }
    
    public IActionResult Excluir(int id)
    {
        var medico = _context.Medicos.Find(id);
        
        if (medico != null)
        {
            return View(new ListarMedicoViewModel()
            {
                Id = medico.Id,
                CRM = medico.CRM,
                Name = medico.Name
            });
        }

        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(int id, EditarMedicoViewModel dados)
    {
        
        var medico = _context.Medicos.Find(id);

        if (medico != null)
        {
            _context.Medicos.Remove(medico);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        return NotFound();
    }
}
