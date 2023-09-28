using Application.Contracts.Persistence;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IAccountServices, AccountServices>()
                .AddTransient<IMessageServices, MessageServices>()
                .AddTransient<ICountryServices, CountryServices>()
                .AddTransient<IStateServices, StateServices>()
                .AddTransient<ICityServices, CityServices>()
                .AddTransient<IZoneServices, ZoneServices>()
                .AddTransient<IPropertyImageServices, PropertyImageServices>()
                .AddTransient<IPropertyTraceServices, PropertyTraceServices>()
                .AddTransient<IPropertyServices, PropertyServices>();
            
            return services;
        }

    }
}
