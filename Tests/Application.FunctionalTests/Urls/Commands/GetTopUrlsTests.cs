using System.Collections.Generic;

namespace Shortener.Tests.Application.FunctionalTests.Urls.Commands;

public class GetTopUrlsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();

    [Fact]
    public async Task Should_ReturnTopUrls_OnRequest()
    {
        // Act
        for (int i = 0; i < 10; i++)
        {
            var url = helper.GenerateValidUrl();
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