using FluentValidation;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Validators.Medicos;

public class AdicionarMedicoValidator : AbstractValidator<AdicionarMedicoViewModel>
{
    public AdicionarMedicoValidator(SisMedContext context)
    {
        RuleFor(x => x.CRM).NotEmpty().WithMessage("CRM é obrigatório")
            .MaximumLength(20).WithMessage("O CRM deve ter no máximo {MaxLength} caracteres.")
            .Must(crm => !context.Medicos.Any(m => m.CRM == crm)).WithMessage("Este CRM já está em uso.");
        
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(200).WithMessage("O Nome deve ter no máximo {MaxLength} caracteres.");
    }
}