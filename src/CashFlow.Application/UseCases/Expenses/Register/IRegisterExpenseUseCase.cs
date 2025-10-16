using CashFlow.Communication.Request;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCases.Expenses.Register
{
  public interface IRegisterExpenseUseCase
  {
    ResponseRegisteredExpenseJson Execute(RequestExpensesJson request);
  }
}

