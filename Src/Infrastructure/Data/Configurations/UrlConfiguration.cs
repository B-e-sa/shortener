using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortener.Domain.Entities;

namespace Shortener.Infrastructure.Data.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.Property(u => u.Visits)
            .HasDefaultValue(0);
    }
}