using Api.Common;
using Api.Features.Admin;
using Api.Features.Movies;
using Api.Features.Reviews;
using Api.Features.Users;
using Api.Middleware;
using Application;
using Infrastructure;
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
    .ReadFrom.Configuration(configuration)
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
        .WithOrigins("http://localhost:3000", "http://localhost:3030")
        .WithExposedHeaders("Location"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSerilog();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapScalarApiReference();

app.MapOpenApi();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.UseMiddleware<TraceMiddleware>();

app.MapGet("api/", () =>
{
    return Results.Ok("Hello there :)");
});

app.MapUsers();
app.MapMovies();
app.MapReviews();
app.MapAdmin();

app.Run();
