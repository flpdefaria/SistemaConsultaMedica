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
    private readonly IValidator<EditarPacienteViewModel> _editarPacienteValidator;
    private const int tamanhoPagina = 10;

    public PacientesController(SisMedContext context, IValidator<AdicionarPacienteViewModel> adicionarPacienteValidator,
        IValidator<EditarPacienteViewModel> editarPacienteValidator)
    {
        _context = context;
        _adicionarPacienteValidator = adicionarPacienteValidator;
        _editarPacienteValidator = editarPacienteValidator;
    }

    public IActionResult Index(string filtro, int pagina = 1)
    {
        var query = _context.Pacientes.AsQueryable();
        
        if(!string.IsNullOrWhiteSpace(filtro))
        {
            query = query.OrderBy(x => x.Name).Where(x => x.Name.Contains(filtro) || x.CPF.Contains(filtro));
        }
        
        var pacientes = query
            .Select(x => new ListarPacienteViewModel
            {
                Id = x.Id,
                CPF = x.CPF,
                Name = x.Name
            });

        ViewBag.Filtro = filtro;
        ViewBag.NumeroPagina = pagina;
        ViewBag.TotalPaginas = Math.Ceiling((decimal)pacientes.Count() / tamanhoPagina);
        
        var pacientesPaginados = pacientes
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();
        
        return View(pacientesPaginados);
        
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

    public IActionResult Editar(int id)
    {
        var paciente = _context.Pacientes.Find(id);

        if (paciente != null)
        {
            var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(x => x.IdPaciente == id);

            return View(new EditarPacienteViewModel
            {
                Id = paciente.Id,
                CPF = paciente.CPF,
                Name = paciente.Name,
                DataNascimento = paciente.DataNascimento,
                Alergias = informacoesComplementares?.Alergias,
                MedicamentosEmUso = informacoesComplementares?.MedicamentosEmUso,
                CirurgiasRealizadas = informacoesComplementares?.CirurgiasRealizadas
            });
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, EditarPacienteViewModel dados)
    {
        var validacao = _editarPacienteValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }

        var paciente = _context.Pacientes.Find(id);

        if (paciente != null)
        {
            paciente.CPF = Regex.Replace(dados.CPF, "[^0-9]", "");
            paciente.Name = dados.Name;
            paciente.DataNascimento = dados.DataNascimento;

            var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(x => x.IdPaciente == id);

            if (informacoesComplementares == null)
            {
                informacoesComplementares = new InformacoesComplementaresPaciente();
            }

            informacoesComplementares.Alergias = dados.Alergias;
            informacoesComplementares.MedicamentosEmUso = dados.MedicamentosEmUso;
            informacoesComplementares.CirurgiasRealizadas = dados.CirurgiasRealizadas;
            informacoesComplementares.IdPaciente = id;

            if (informacoesComplementares.Id > 0)
            {
                _context.InformacoesComplementaresPaciente.Update(informacoesComplementares);
            }
            else
            {
                _context.InformacoesComplementaresPaciente.Add(informacoesComplementares);
            }

            _context.Pacientes.Update(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return NotFound();
    }

    public IActionResult Excluir(int id)
    {
        var paciente = _context.Pacientes.Find(id);

        if (paciente != null)
        {
            return View(new ListarPacienteViewModel
            {
                Id = paciente.Id,
                CPF = paciente.CPF,
                Name = paciente.Name
            });
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(int id, EditarPacienteViewModel dados)
    {
        var paciente = _context.Pacientes.Find(id);

        if (paciente != null)
        {
            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return NotFound();
    }

    public IActionResult Buscar(string filtro)
    {
        var pacientes = _context.Pacientes
            .Where(x => x.Name.Contains(filtro) || x.CPF.Contains(filtro))
            .Take(10)
            .Select(x => new ListarPacienteViewModel
            {
                Id = x.Id,
                CPF = x.CPF,
                Name = x.Name
            });

        return Json(pacientes);
    }
}