using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Application.Abstractions;
using Transaction.Infrastructure.Messaging;
using Transaction.Infrastructure.Persistence;
using Transaction.Infrastructure.Persistence.Repositories;

namespace Transaction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<TransactionDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("TransactionConnection")));

        // Repositories
        services.AddScoped<ITransactionRepository>(sp =>
            new TransactionRepository(
                sp.GetRequiredService<TransactionDbContext>(),
                configuration.GetConnectionString("TransactionConnection")!));

        services.AddScoped<ITransactionLimitRepository, TransactionLimitRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}