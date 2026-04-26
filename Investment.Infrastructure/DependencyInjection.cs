using Investment.Application.Abstractions;
using Investment.Infrastructure.Messaging;
using Investment.Infrastructure.Persistence;
using Investment.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInvestmentInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<InvestmentDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("InvestmentConnection")));

        // Repositories
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();

        services.AddScoped<IAssetRepository>(sp =>
            new AssetRepository(
                sp.GetRequiredService<InvestmentDbContext>(),
                configuration.GetConnectionString("InvestmentConnection")!));

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}