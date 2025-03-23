
using Infrastructure.Database;

using Microsoft.EntityFrameworkCore;

using Serilog;

namespace Api.Extensions;

public static class AppExtensions
{
    public static async Task UpdatePendingMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CineSlateContext>();

        try
        {
            var migrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (!migrations.Any())
            {
                Log.Information("No penidng migrations");
                return;
            }

            Log.Information("Pending migrations: ");

            foreach (var migration in migrations)
            {
                Log.Information(migration);
            }

            await dbContext.Database.MigrateAsync();

            Log.Information("Finished updating.");
        }
        catch (Exception ex)
        {
            Log.Error("Updating migrations failed with message: {0}", ex.Message);
        }
    }
}