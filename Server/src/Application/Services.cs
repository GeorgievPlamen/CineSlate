using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServices).Assembly;
        services.AddMediatR(assembly);

        return services;
    }
}