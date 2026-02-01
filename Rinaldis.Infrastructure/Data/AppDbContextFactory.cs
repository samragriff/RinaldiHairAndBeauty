using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Rinaldis.Infrastructure.Data;

/// <summary>
/// Used by EF Core tools (e.g. dotnet ef) so migrations work without setting ASPNETCORE_ENVIRONMENT.
/// Loads connection string from Rinaldis.API appsettings (and appsettings.Development.json).
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Rinaldis.API");
        if (!Directory.Exists(basePath))
            basePath = Directory.GetCurrentDirectory();

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = config.GetConnectionString("Default")
            ?? throw new InvalidOperationException("ConnectionStrings:Default not found. Add it to Rinaldis.API appsettings.json or appsettings.Development.json.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
