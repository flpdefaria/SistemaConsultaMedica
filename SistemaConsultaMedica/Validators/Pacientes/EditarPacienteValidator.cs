using System.Text.RegularExpressions;
using FluentValidation;
using SistemaConsultaMedica.Models.Contexts;
using SistemaConsultaMedica.ViewModels.Pacientes;

namespace SistemaConsultaMedica.Validators.Pacientes;

public class EditarPacienteValidator : AbstractValidator<EditarPacienteViewModel>
{
    public EditarPacienteValidator(SisMedContext context)
    {
        RuleFor(x => x.CPF)
            .NotEmpty()
            .WithMessage("CPF é obrigatório")
            .Must(cpf => !string.IsNullOrWhiteSpace(cpf) && Regex.Replace(cpf, "[^0-9]", "").Length == 11)
            .WithMessage("O CPF deve ter 11 dígitos.");
            
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Campo obrigatório")
            .MaximumLength(200).WithMessage("O Nome deve ter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty()
            .WithMessage("Data de Nacismento é obrigatório")
            .Must(data => data <= DateTime.Today)
            .WithMessage("A data de nascimento deve ser menor ou igual a data atual.");
        
        RuleFor(x => x)
            .Must(x => !context.Pacientes.Any(m => m.CPF == Regex.Replace(x.CPF, "[^0-9]", "") && m.Id != x.Id))
            .WithMessage("Este CPF já está em uso.");
    }
}