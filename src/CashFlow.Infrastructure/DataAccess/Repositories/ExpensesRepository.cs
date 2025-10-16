using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository(CashFlowDbContext context) : IExpensesRepository
    {
        public void Add(Expense expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }
}
