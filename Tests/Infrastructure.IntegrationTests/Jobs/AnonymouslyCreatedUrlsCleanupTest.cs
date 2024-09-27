using Moq;
using System.Linq;
using System.Collections.Generic;
using Shortener.Infrastructure.Jobs;
using Shortener.Application.Common.Interfaces;
using System;
using System.Threading;
using MockQueryable.Moq;

namespace Shortener.Tests.Infrastructure.IntegrationTests.Jobs;

public class AnonymouslyCreatedUrlsCleanupJobTests
{
    [Fact]
    public async Task Should_DeleteUrlsOlderThan7Days()
    {
        // Arrange
        var mockDbContext = new Mock<IAppDbContext>();

        var oldUrls = new List<Url> {
            new() { CreatedAt = DateTime.Now, UserId = null },
            new() { CreatedAt = DateTime.Now.AddDays(-4), UserId = null },
            new() { CreatedAt = DateTime.Now.AddDays(-8), UserId = null },
            new() { CreatedAt = DateTime.Now.AddDays(-24), UserId = null }
        };

        var mockUrlSet = oldUrls.AsQueryable().BuildMockDbSet();
        mockDbContext.Setup(db => db.Urls).Returns(mockUrlSet.Object);

        var job = new AnonymouslyCreatedUrlsCleanupJob(mockDbContext.Object);

        mockDbContext.Setup(db => db.Urls.RemoveRange(It.IsAny<IEnumerable<Url>>()))
            .Callback<IEnumerable<Url>>(urls => oldUrls.RemoveAll(u => urls.Contains(u)));

        // Act
        await job.Execute(null);

        // Assert
        mockDbContext.Verify(db => db.Urls.RemoveRange(It.Is<IEnumerable<Url>>(urls => urls.Count() == 2)), Times.Once);
        mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        oldUrls.Should().HaveCount(2);
        oldUrls.Should().OnlyContain(u => u.CreatedAt > DateTime.Now.AddDays(-7));
    }
}
