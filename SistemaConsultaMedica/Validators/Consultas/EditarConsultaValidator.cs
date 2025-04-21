using FluentValidation;
using SistemaConsultaMedica.ViewModels.Consultas;

namespace SistemaConsultaMedica.Validators.Consultas;

public class EditarConsultaValidator : AbstractValidator<EditarConsultaViewModel>
{
    public EditarConsultaValidator()
    {
        RuleFor(x => x.Data).NotEmpty().WithMessage("Data é obrigatório")
            .Must(data => data <= DateTime.Today).WithMessage("A data não pode ser maior que a data atual.");

        RuleFor(x => x.Tipo).NotNull().WithMessage("Campo obrigatório");

        RuleFor(x => x.IdPaciente).NotEmpty().WithMessage("Campo obrigatório");

        RuleFor(x => x.IdMedico).NotEmpty().WithMessage("Campo obrigatório");
    }
}