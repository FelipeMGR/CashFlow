using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exceptions.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase(IExpensesRepository repository, IUnityOfWork uof) : IRegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestExpensesJson request)
        {
            Validate(request);

            Expense entity = new()
            {
                Amount = request.Amount,
                Title = request.Title,
                Description = request.Description,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType
            };

            repository.Add(entity);
            uof.CommitChanges();

            return new ResponseRegisteredExpenseJson();
        }

        private static void Validate(RequestExpensesJson request)
        {
            RegisterExpenseValidator validator = new();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                List<string> errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
