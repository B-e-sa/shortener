using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shortener.Infrastructure.Data;
using Testcontainers.PostgreSql;

namespace Shortener.Tests.Application.FunctionalTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly string rootPath = Directory
        .GetParent(Directory.GetCurrentDirectory())
        !.Parent
        !.Parent
        !.Parent
        !.FullName;

    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("shortener-api")
        .WithUsername("shortener-admin")
        .WithPassword("admin")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot($"{rootPath}/Tests");
        builder.ConfigureTestServices(s =>
        {
            s.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            s.AddDbContext<AppDbContext>(o =>
            {
                o.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile($"{rootPath}/Src/Web/appsettings.json", optional: false, reloadOnChange: true);
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}