using Microsoft.EntityFrameworkCore;
using Shortener.Models;

namespace Shortener.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Url>()
                .Property(u => u.Visits)
                .HasDefaultValue(0);
        }

        public DbSet<Url> Urls { get; set; }
    }
}