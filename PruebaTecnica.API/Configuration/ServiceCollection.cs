namespace PruebaTecnica.Application.Configuration;

public static class ServiceCollection
{
    /// <summary>
    /// Provides extension methods for configuring application services.
    /// This class adds necessary services such as MediatR, database context, 
    /// Unit of Work, and repositories to the service collection.
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.Load("PruebaTecnica.Application")));
        
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
