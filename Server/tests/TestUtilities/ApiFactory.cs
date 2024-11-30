using Api.Common;
using Application.Movies.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Models.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Testcontainers.PostgreSql;
using Xunit;

namespace TestUtilities;

public class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("testDB")
        .WithUsername("postgres")
        .WithPassword("secretpassword")
        .Build();

    public async Task SeedDatabaseAsync(IEnumerable<BaseModel> models)
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CineSlateContext>();
        context.AddRange(models);
        await context.SaveChangesAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var moviesClientSubstitute = Substitute.For<IMovieClient>();
            services.AddSingleton(moviesClientSubstitute);

            var descriptor = services.FirstOrDefault(
                s => s.ServiceType == typeof(DbContextOptions<CineSlateContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<CineSlateContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString()));

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CineSlateContext>();
            dbContext.Database.Migrate();
        });
    }

    public IMovieClient MoviesClientMock => Services.GetRequiredService<IMovieClient>();

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
