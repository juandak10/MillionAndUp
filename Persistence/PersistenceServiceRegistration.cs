﻿using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataBase;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MillionAndUpContext>(options => options.UseSqlServer(configuration.GetConnectionString("MillionAndUpDB"), b => b.MigrationsAssembly("Persistence")));

            services
                .AddTransient<IAccountRepository, AccountRepository>()
                .AddTransient<IMessageRepository, MessageRepository>()
                .AddTransient<ICityRepository, CityRepository>()
                .AddTransient<ICountryRepository, CountryRepository>()
                .AddTransient<IPropertyImageRepository, PropertyImageRepository>()
                .AddTransient<IPropertyRepository, PropertyRepository>()
                .AddTransient<IStateRepository, StateRepository>()
                .AddTransient<IZoneRepository, ZoneRepository>()
                .AddTransient<IPropertyTraceRepository, PropertyTraceRepository>();
            
            return services;
        }

    }
}
