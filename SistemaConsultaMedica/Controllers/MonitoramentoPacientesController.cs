using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.Models.Entities;
using SistemaConsultaMedica.Validators.MonitoramentoPaciente;
using SistemaConsultaMedica.ViewModels.MonitoramentoPaciente;

namespace SistemaConsultaMedica.Controllers;

[Route("Monitoramento")]

public class MonitoramentoPacientesController : Controller
{
    private readonly SisMedContext _context;
    private readonly IValidator<AdicionarMonitoramentoViewModel> _adicionarMonitoramentoValidator;
    private readonly IValidator<EditarMonitoramentoViewModel> _editarMonitoramentoValidator;

    public MonitoramentoPacientesController(SisMedContext context,
        IValidator<AdicionarMonitoramentoViewModel> adicionarMonitoramentoValidator,
        IValidator<EditarMonitoramentoViewModel> editarMonitoramentoValidator)
    {
        _context = context;
        _adicionarMonitoramentoValidator = adicionarMonitoramentoValidator;
        _editarMonitoramentoValidator = editarMonitoramentoValidator;
    }

    public IActionResult Index(int idPaciente)
    {
        ViewBag.IdPaciente = idPaciente;
        var monitoramentos = _context.MonitoriamentoPaciente
            .Where(x => x.IdPaciente == idPaciente)
            .Select(x => new ListarMonitoramentoViewModel
            {
                Id = x.Id,
                PressaoArterial = x.PressaoArterial,
                SaturacaoOxigenio = x.SaturacaoOxigenio,
                Temperatura = x.Temperatura,
                FrequenciaCardiaca = x.FrequenciaCardiaca,
                DataAfericao = x.DataAfericao
            });

        return View(monitoramentos);
    }

    [Route("Adicionar")]
    public IActionResult Adicionar(int idPaciente)
    {
        ViewBag.IdPaciente = idPaciente;
        return View();
    }

    [Route("Adicionar")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(int idPaciente, AdicionarMonitoramentoViewModel dados)
    {
        var validacao = _adicionarMonitoramentoValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }

        var monitoramento = new MonitoriamentoPaciente
        {
            PressaoArterial = dados.PressaoArterial,
            SaturacaoOxigenio = dados.SaturacaoOxigenio,
            Temperatura = dados.Temperatura,
            FrequenciaCardiaca = dados.FrequenciaCardiaca,
            DataAfericao = dados.DataAfericao,
            IdPaciente = idPaciente
        };

        _context.MonitoriamentoPaciente.Add(monitoramento);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index), new { idPaciente });
    }

    [Route("Editar/{id}")]
    public IActionResult Editar(int id)
    {
        var monitoramento = _context.MonitoriamentoPaciente.Find(id);

        if (monitoramento != null)
        {
            return View(new EditarMonitoramentoViewModel
            {
                Id = monitoramento.Id,
                PressaoArterial = monitoramento.PressaoArterial,
                SaturacaoOxigenio = monitoramento.SaturacaoOxigenio,
                Temperatura = monitoramento.Temperatura,
                FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                DataAfericao = monitoramento.DataAfericao,
            });
        }

        return NotFound();
    }

    [Route("Editar/{id}")]
    [HttpPost]
    public IActionResult Editar(int id, EditarMonitoramentoViewModel dados)
    {
        var validacao = _editarMonitoramentoValidator.Validate(dados);

        if (!validacao.IsValid)
        {
            validacao.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }

        var monitoramento = _context.MonitoriamentoPaciente.Find(id);

        if (monitoramento != null)
        {
            monitoramento.PressaoArterial = dados.PressaoArterial;
            monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
            monitoramento.Temperatura = dados.Temperatura;
            monitoramento.FrequenciaCardiaca = dados.FrequenciaCardiaca;
            monitoramento.DataAfericao = dados.DataAfericao;

            _context.MonitoriamentoPaciente.Update(monitoramento);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { monitoramento.IdPaciente });
        }

        return NotFound();
    }
    
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
        var monitoramento = _context.MonitoriamentoPaciente.Find(id);

        if (monitoramento != null)
        {
            return View(new ListarMonitoramentoViewModel
            {
                Id = monitoramento.Id,
                PressaoArterial = monitoramento.PressaoArterial,
                SaturacaoOxigenio = monitoramento.SaturacaoOxigenio,
                Temperatura = monitoramento.Temperatura,
                FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                DataAfericao = monitoramento.DataAfericao,
            });
        }

        return NotFound();
    }

    [Route("Excluir/{id}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(int id, ListarMonitoramentoViewModel dados)
    {
        var monitoramento = _context.MonitoriamentoPaciente.Find(id);

        if (monitoramento != null)
        {
            _context.MonitoriamentoPaciente.Remove(monitoramento);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { monitoramento.IdPaciente});
        }

        return NotFound();
    }

}
