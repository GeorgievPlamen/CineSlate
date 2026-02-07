using Application.Common.Interfaces;
using Application.Common.PipelineBehaviours;

using FluentValidation;
using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using AppContext = Application.Common.Context.AppContext;

namespace Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServices).Assembly;

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IAppContext, AppContext>();

        return services;
    }
}