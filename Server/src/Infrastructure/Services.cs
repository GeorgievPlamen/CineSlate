using Application.Common.Interfaces;
using Application.Users.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddDbContext<CineSlateContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CineSlate")));

        return services;
    }
}