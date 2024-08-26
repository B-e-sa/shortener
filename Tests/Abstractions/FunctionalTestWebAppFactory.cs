using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shortener.Infrastructure;
using Testcontainers.PostgreSql;
using Xunit;

namespace Shortener.Tests.Abstractions
{
    public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private static readonly string parentPath = Directory
            .GetParent(Directory.GetCurrentDirectory())
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
            builder.UseContentRoot(parentPath);
            builder.ConfigureTestServices(s =>
            {
                s.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                s.AddDbContext<AppDbContext>(o =>
                {
                    o.UseNpgsql(_dbContainer.GetConnectionString());
                });
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
}