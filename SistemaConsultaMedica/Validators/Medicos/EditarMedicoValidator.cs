using FluentValidation;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Medicos;

namespace SistemaConsultaMedica.Validators.Medicos;

public class EditarMedicoValidator : AbstractValidator<EditarMedicoViewModel>
{
    public EditarMedicoValidator(SisMedContext context)
    {
        RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo obrigatório")
            .MaximumLength(20).WithMessage("O CRM deve ter no máximo {MaxLength} caracteres.");
        
        RuleFor(x => x.Name).NotEmpty().WithMessage("Campo obrigatório")
            .MaximumLength(200).WithMessage("O Nome deve ter no máximo {MaxLength} caracteres.");
        
        RuleFor(x => x).Must(x => !context.Medicos.Any(m => m.CRM == x.CRM && m.Id != x.Id)).WithMessage("Este CRM já está em uso.");
    }
}