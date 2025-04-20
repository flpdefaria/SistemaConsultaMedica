using FluentValidation;
using SistemaConsultaMedica.ViewModels.Consultas;

namespace SistemaConsultaMedica.Validators.Consultas;

public class AdicionarConsultaValidator :  AbstractValidator<AdicionarConsultaViewModel>
{
    public AdicionarConsultaValidator()
    {
        RuleFor(x => x.Data).NotEmpty().WithMessage("Data é obrigatório")
            .Must(data => data <= DateTime.Today).WithMessage("A data não pode ser maior que a data atual.");
    }
}