using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension //this class is used to register the infrastructure services
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            AddRepositories(services);
            AddDbContext(services, config); 
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("Connection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
