using Loan.Application.Abstractions;
using Loan.Infrastructure.Messaging;
using Loan.Infrastructure.Persistence;
using Loan.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddLoanInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<LoanDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("LoanConnection")));

        // Repository
        services.AddScoped<ILoanRepository, LoanRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}