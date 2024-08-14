using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Data;
using System.Threading.Tasks;
using Xunit;

namespace Shortener.Tests.Abstractions
{
    public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
    {
        protected HttpClient HttpClient { get; init; }
        protected AppDbContext DbContext { get; init; }

        public BaseFunctionalTest(FunctionalTestWebAppFactory factory)
        {
            HttpClient = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            DbContext.Database.Migrate();
        }
    }
}