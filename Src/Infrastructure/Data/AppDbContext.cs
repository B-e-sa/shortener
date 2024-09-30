using System.Reflection;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shortener.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options), IAppDbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Url> Urls => Set<Url>();

    public DbSet<NewPasswordRequest> NewPasswordRequests => Set<NewPasswordRequest>();

    public DbSet<EmailVerification> EmailVerifications => Set<EmailVerification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}