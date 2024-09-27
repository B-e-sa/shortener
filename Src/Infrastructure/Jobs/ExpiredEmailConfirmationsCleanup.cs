using Microsoft.EntityFrameworkCore;
using Quartz;
using Shortener.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Infrastructure.Jobs;

public class ExpiredEmailConfirmationsCleanup(IAppDbContext dbContext) : IJob
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Execute(IJobExecutionContext context)
    {
        var expiredEmails = await _dbContext
            .EmailVerifications
            .Where(e => (DateTime.Now - e.CreatedAt).TotalDays > 5)
            .ToListAsync();

        _dbContext.EmailVerifications.RemoveRange(expiredEmails);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}