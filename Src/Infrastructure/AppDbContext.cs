using Microsoft.EntityFrameworkCore;
using Shortener.Domain.Entities;

namespace Shortener.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Url>()
            .Property(u => u.Visits)
            .HasDefaultValue(0);

        builder.Entity<Url>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("now()");
    }

    public DbSet<Url> Urls { get; set; }
}