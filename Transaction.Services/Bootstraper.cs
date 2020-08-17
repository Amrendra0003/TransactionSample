using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Repository;

namespace Transaction.Services
{
    public class Bootstraper
    {
        public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            InitializeRepository(services, configuration);
            services.AddScoped<ITransactionService, TransactionService>();
        }
        public static void InitializeRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
        
    }
}
