using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Infrastructure.Services.FireBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFireBaseServices, FireBaseServices>();
            return services;
        }

    }
}
