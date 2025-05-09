using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.ToDoCases.Command;
using FluentValidation;

namespace Application.UseCases.ToDoCases.Dto.Validation
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today).When(x => x.DueDate.HasValue)
                .WithMessage("DueDate cannot be in the past.");
        }
    }
}
