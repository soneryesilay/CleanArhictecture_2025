using CleanArhictecture_2025.Domain.Employees;
using CleanArhictecture_2025.Infrastructure.Context;
using CleanArhictecture_2025.Infrastructure.Repositories;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArhictecture_2025.Infrastructure;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;
            opt.UseSqlServer(connectionString);

        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.Scan(opt => opt
           .FromAssemblies(typeof(InfrastructureRegistrar).Assembly)
           .AddClasses(publicOnly: false)
           .UsingRegistrationStrategy(RegistrationStrategy.Skip)
           .AsImplementedInterfaces()
           .WithScopedLifetime());
           
      
        return services;
    }
}
