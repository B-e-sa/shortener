using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Users.Abstractions;
using Shortener.Infrastructure.Authentication.Jwt;
using Shortener.Infrastructure.Data;
using Shortener.Infrastructure.Data.Interceptors;
using Shortener.Infrastructure.Encryption;

namespace Shortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        Guard.Against.Null(connectionString, message: "Connection string 'Database' not found.");

        services.AddScoped<ISaveChangesInterceptor, BaseEntityInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options
                .UseNpgsql(connectionString)
                .AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
        });

        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<AppDbContextInitialiser>();

        services.AddScoped<IEncryptionProvider, EncryptionProvider>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}