using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shortener.Application.Common.Interfaces;
using Shortener.Infrastructure.Authentication.Jwt;
using Shortener.Infrastructure.Data;
using Shortener.Infrastructure.Data.Interceptors;
using Shortener.Infrastructure.Encryption;
using Shortener.Infrastructure.Jobs;
using Shortener.Infrastructure.Mailing;

namespace Shortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

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

        services.AddScoped<IMailingProvider, MailingProvider>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddQuartz(x =>
        {
            var expiredConfirmationsJobKey = JobKey.Create(nameof(ExpiredEmailConfirmationsCleanup));
            var anonymouslyCreatedUrlsCleanupJobKey = JobKey.Create(nameof(AnonymouslyCreatedUrlsCleanupJob));

            x
            .AddJob<ExpiredEmailConfirmationsCleanup>(expiredConfirmationsJobKey)
            .AddTrigger(t =>
            {
                t
                .ForJob(expiredConfirmationsJobKey)
                .WithSimpleSchedule(s => s.WithIntervalInHours(48).RepeatForever());
            })
            .AddJob<AnonymouslyCreatedUrlsCleanupJob>(anonymouslyCreatedUrlsCleanupJobKey)
            .AddTrigger(t =>
            {
                t.ForJob(anonymouslyCreatedUrlsCleanupJobKey)
                .WithSimpleSchedule(s => s.WithIntervalInHours(120).RepeatForever());
            });
        });

        return services;
    }
}