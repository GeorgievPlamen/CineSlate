using Api.Common;
using Api.Extensions;
using Api.Features.Admin;
using Api.Features.Movies;
using Api.Features.Reviews;
using Api.Features.Users;
using Api.Middleware;

using Application;
using Application.Common.Tracing;

using Infrastructure;
using Infrastructure.Common.Models;

using Microsoft.Extensions.Options;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Scalar.AspNetCore;

using Serilog;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(configuration)
    .CreateLogger();

Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", "http://localhost:3030", "https://orange-glacier-08896bc03.6.azurestaticapps.net")
        .WithExposedHeaders("Location"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSerilog();
builder.Services.AddOpenApi();

var appConfig = builder.Configuration.GetSection("App").Get<App>();
if (appConfig != null)
{
    builder.Services.AddSingleton(Options.Create(appConfig));
    builder.Logging.AddOpenTelemetry(opt =>
    {
        opt.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(serviceName: appConfig.Name, serviceVersion: appConfig?.Version))
            .AddConsoleExporter()
            .AddOtlpExporter();
    });
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(r => r
            .AddService(serviceName: appConfig.Name, serviceVersion: appConfig?.Version))
        .WithTracing(t => t
            .AddSource(appConfig.Name)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter())
        .WithMetrics(m => m
            .AddMeter(appConfig.Name)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddPrometheusExporter()
            .AddOtlpExporter());
}

var app = builder.Build();

app.MapScalarApiReference();

app.MapOpenApi();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.UseMiddleware<TraceMiddleware>();
app.MapPrometheusScrapingEndpoint();

app.MapGet("api/", () => Results.Ok("Hello there :)"));

app.MapUsers();
app.MapMovies();
app.MapReviews();
app.MapAdmin();

await app.UpdatePendingMigrations();

app.Run();
