using Bogus;
using Newtonsoft.Json;

namespace Shortener.Tests.Application.FunctionalTests.Abstractions;

public class TestHelper(string currentRoute)
{
    private readonly string CurrentRoute = currentRoute;
    protected readonly Faker faker = new();

    public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public string GetApiUrl() => $"http://localhost:5229/api/{CurrentRoute}";
}