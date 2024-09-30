using Moq;
using System.Linq;
using System.Collections.Generic;
using Shortener.Infrastructure.Jobs;
using Shortener.Application.Common.Interfaces;
using System;
using System.Threading;
using MockQueryable.Moq;

namespace Shortener.Tests.Infrastructure.IntegrationTests.Jobs;

public class ExpiredEmailConfirmationsCleanupTest
{
    [Fact]
    public async Task Should_DeleteExpiredEmailConfirmationsOlderThan2Days()
    {
        // Arrange
        var mockDbContext = new Mock<IAppDbContext>();

        var oldVerifications = new List<EmailVerification> {
            new() { CreatedAt = DateTime.Now, UserId = 0 },
            new() { CreatedAt = DateTime.Now.AddDays(-4), UserId = 18 },
            new() { CreatedAt = DateTime.Now.AddDays(-7), UserId = 24 },
            new() { CreatedAt = DateTime.Now.AddDays(-6), UserId = 1 }
        };

        var mockUrlSet = oldVerifications.AsQueryable().BuildMockDbSet();
        mockDbContext.Setup(db => db.EmailVerifications).Returns(mockUrlSet.Object);

        var job = new ExpiredEmailConfirmationsCleanup(mockDbContext.Object);

        mockDbContext
            .Setup(db => db
                .EmailVerifications
                .RemoveRange(It.IsAny<IEnumerable<EmailVerification>>()))
            .Callback<IEnumerable<EmailVerification>>(verification => 
                oldVerifications.RemoveAll(u => verification.Contains(u)));

        // Act
        await job.Execute(null);

        // Assert
        mockDbContext
            .Verify(db => db
                .EmailVerifications
                .RemoveRange(It.Is<IEnumerable<EmailVerification>>(urls => urls.Count() == 2)), Times.Once);

        mockDbContext
            .Verify(db => db
                .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        oldVerifications.Should().HaveCount(2);
        oldVerifications.Should().OnlyContain(u => u.CreatedAt > DateTime.Now.AddDays(-5));
    }
}
