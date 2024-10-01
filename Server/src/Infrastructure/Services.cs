using Application.Users.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // var assembly = typeof(InfrastructureServices).Assembly;
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}