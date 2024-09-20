using Microsoft.EntityFrameworkCore;
using Shortener.Domain.Entities;

namespace Shortener.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }

    DbSet<Url> Urls { get; }

    DbSet<EmailVerification> EmailVerifications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}