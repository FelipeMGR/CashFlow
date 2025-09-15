using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Exceptions.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestExpensesJson request)
        {
            Validate(request);

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
