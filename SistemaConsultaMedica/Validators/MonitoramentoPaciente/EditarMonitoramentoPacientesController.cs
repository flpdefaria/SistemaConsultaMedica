using FluentValidation;
using SistemaConsultaMedica.ViewModels.MonitoramentoPaciente;

namespace SistemaConsultaMedica.Validators.MonitoramentoPaciente;

public class EditarMonitoramentoValidator : AbstractValidator<EditarMonitoramentoViewModel>
{
    public EditarMonitoramentoValidator()
    {
        RuleFor(x => x.PressaoArterial).NotEmpty().WithMessage("Pressão Arterial é obrigatório");

        RuleFor(x => x.SaturacaoOxigenio).NotEmpty().WithMessage("Saturação Oxigenio é obrigatório")
            .Must(saturacao => saturacao >= 0 && saturacao <= 100).WithMessage("Saturação Oxigenio deve estar entre 0 e 100");
        
        RuleFor(x => x.Temperatura).NotEmpty().WithMessage("Temperatura é obrigatório")
            .Must(temperatura => temperatura > 0).WithMessage("Temperatura deve estar entre 0 e 100");
        
        RuleFor(x => x.FrequenciaCardiaca).NotEmpty().WithMessage("Frequência Cardíaca é obrigatório")
            .Must(frequencia => frequencia >= 0).WithMessage("Frequência Cardíaca deve estar entre 0 e 200");
        
        RuleFor(x => x.DataAfericao).NotEmpty().WithMessage("Data de Aferição é obrigatório")
            .Must(data => data <= DateTime.Now).WithMessage("A data de aferição deve ser menor ou igual a data atual.");
            
    }
}