using Identity.Application.Abstractions;
using Identity.Infrastructure.Authentication;
using Identity.Infrastructure.Messaging;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("IdentityConnection")));

        // Repositories
        services.AddScoped<IUserRepository>(sp =>
            new UserRepository(
                sp.GetRequiredService<IdentityDbContext>(),
                configuration.GetConnectionString("IdentityConnection")!));

        services.AddScoped<IRoleRepository, RoleRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Authentication
        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        // Messaging
        services.AddSingleton(_ =>
            new RabbitMqEventBus(
                configuration["RabbitMQ:HostName"] ?? "localhost"));

        return services;
    }
}