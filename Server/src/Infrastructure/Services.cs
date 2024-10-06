using System.Text;
using Application.Common.Interfaces;
using Application.Users.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IUserIdentity, UserIdentity>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        services.AddDbContext<CineSlateContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CineSlate")));
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddHttpContextAccessor();

        return services;
    }
}