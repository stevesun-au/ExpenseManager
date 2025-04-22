using ExpenseManager.Server.DTOs;
using FluentValidation;

namespace ExpenseManager.Server.Validator
{
    public class ExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description must be at most 200 characters");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("A valid person must be selected");

            RuleFor(x => x.CategoryIds)
                .NotNull().WithMessage("Categories are required")
                .Must(c => c.Any()).WithMessage("At least one category must be selected");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future");
        }
    }
}
