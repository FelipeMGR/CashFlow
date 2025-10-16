using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnityOfWork(CashFlowDbContext context) : IUnityOfWork
    {
        public void CommitChanges() => context.SaveChanges();
    }
}
