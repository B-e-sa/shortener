using System.Collections.Generic;
using Shortener.Application.Url.Commands.CreateUrl;

namespace Shortener.Tests.Application.FunctionalTests.Urls.Commands;

public class GetTopUrlsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly Helper helper = new("url");

    [Fact]
    public async Task Should_ReturnTopUrls_OnRequest()
    {
        // Arrange
        var url = new CreateUrlCommand()
        {
            Title = "New Url",
            Url = "https://www.google.com"
        };

        // Act
        for (int i = 0; i < 10; i++)
        {
            await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
        }

        // TODO: assert that the results are being returned
        //       in descending order

        var res = await HttpClient.GetAsync(helper.GetApiUrl());

        // Assert
        var resBody = await helper.DeserializeResponse<List<Url>>(res);
        resBody.Count.Should().Be(10);
    }
}