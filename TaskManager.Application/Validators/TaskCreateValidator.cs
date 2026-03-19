using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators;

public class TaskCreateValidator : AbstractValidator<TaskCreateDTO>
{
    public TaskCreateValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MinimumLength(3).WithMessage("Mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Máximo 100 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(500);

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 2)
            .WithMessage("Status inválido");

        RuleFor(x => x.Prioridade)
            .InclusiveBetween(0, 2)
            .WithMessage("Prioridade inválida");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("UsuarioId é obrigatório");
    }
}