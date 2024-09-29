using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000");
});

app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok("Hello there :)");
});

app.Run();
