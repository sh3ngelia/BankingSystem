using Card.Application.Abstractions;
using Card.Infrastructure.Messaging;
using Card.Infrastructure.Persistence;
using Card.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Card.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCardInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<CardDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CardConnection")));

        // Repository
        services.AddScoped<ICardRepository, CardRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}