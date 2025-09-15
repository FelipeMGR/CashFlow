using CashFlow.Communication.Request;
using CashFlow.Exceptions;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestExpensesJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expenses => expenses.Title).NotEmpty().WithMessage(ResourcesErrorMessages.TITLE_REQUIRED);
            RuleFor(expenses => expenses.Amount).GreaterThan(0).WithMessage(ResourcesErrorMessages.AMOUNT_LOWER_THAN_0);
            RuleFor(expenses => expenses.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourcesErrorMessages.DATETIME_ERROR);
            RuleFor(expenses => expenses.PaymentType).IsInEnum().WithMessage(ResourcesErrorMessages.PAYMENTTYPE_ERROR);
        }
    }
}
