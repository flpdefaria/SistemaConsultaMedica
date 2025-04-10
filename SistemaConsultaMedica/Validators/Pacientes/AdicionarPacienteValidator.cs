using System.Text.RegularExpressions;
using FluentValidation;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Pacientes;

namespace SistemaConsultaMedica.Validators.Pacientes;

public class AdicionarPacienteValidator : AbstractValidator<AdicionarPacienteViewModel>
{
    public AdicionarPacienteValidator(SisMedContext context)
    {
        RuleFor(x => x.CPF).NotEmpty().WithMessage("CPF é obrigatório")
            .Must(cpf => !string.IsNullOrWhiteSpace(cpf) && Regex.Replace(cpf, "[^0-9]", "").Length == 11).WithMessage("O CPF deve ter 11 dígitos.")
            .Must(cpf => !context.Pacientes.Any(p => p.CPF == cpf)).WithMessage("Este CPF já esta em uso.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(200).WithMessage("O Nome deve ter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Data de Nacismento é obrigatório")
            .Must(data => data <= DateTime.Today).WithMessage("A data de nascimento deve ser menor ou igual a data atual.");
    }
}