using Application.Common.Interfaces;
using Infrastructure.Repositories.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // var assembly = typeof(InfrastructureServices).Assembly;
        services.AddScoped<ITestRepository, TestRepository>();

        return services;
    }
}