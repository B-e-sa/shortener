using Microsoft.EntityFrameworkCore;
using Shortener.Models;

namespace Shortener.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Url> Urls { get; set; }
    }
}