using Application.Common.PipelineBehaviours;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServices).Assembly;

        services.AddMediatR(assembly);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        return services;
    }
}