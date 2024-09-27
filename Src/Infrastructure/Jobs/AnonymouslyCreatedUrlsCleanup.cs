using Microsoft.EntityFrameworkCore;
using Quartz;
using Shortener.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Infrastructure.Jobs;

public class AnonymouslyCreatedUrlsCleanupJob(IAppDbContext dbContext) : IJob
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Execute(IJobExecutionContext context)
    {
        var urlsToDelete = await _dbContext
            .Urls
            .Where(u => u.UserId == null && (DateTime.Now - u.CreatedAt).TotalDays >= 7)
            .ToListAsync();

        _dbContext.Urls.RemoveRange(urlsToDelete);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}
