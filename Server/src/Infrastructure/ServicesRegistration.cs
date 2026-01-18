using System.Text;

using Api.Common.Interfaces;

using Application.Common.Interfaces;
using Application.Movies.Interfaces;
using Application.Notifications.Interfaces;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;
using Application.Watchlist.Interfaces;

using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Database;
using Infrastructure.MovieClient;
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
        ConfigurationManager configuration,
        bool isDevelopment)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.Configure<ApiKeys>(configuration.GetSection(nameof(ApiKeys)));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/notifications"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddDbContext<CineSlateContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("CineSlate"));

            if (isDevelopment)
                options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IWatchlistRepository, WatchlistRepository>();
        services.AddScoped<INotificaitonRepository, NotificationRepository>();

        services.AddScoped<IMovieClient, TMDBClient>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<IUserIdentity, UserIdentity>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}