using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.Load("PruebaTecnica.Application")));
        
        // Obtener la cadena de conexión
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly("PruebaTecnica.Infrastructure")
            ));
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));
        services.AddScoped<IBankRepository>(provider => new BankRepository(connectionString));

        return services;
    }
}
