using Account.Application.Abstractions;
using Account.Infrastructure.Messaging;
using Account.Infrastructure.Persistence;
using Account.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("AccountConnection")));

        // Repository
        services.AddScoped<IAccountRepository>(sp =>
            new AccountRepository(
                sp.GetRequiredService<AccountDbContext>(),
                configuration.GetConnectionString("AccountConnection")!));

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}