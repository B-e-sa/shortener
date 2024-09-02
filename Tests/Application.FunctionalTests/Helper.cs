using System.Net.Http;
using Newtonsoft.Json;

namespace Shortener.Tests.Application.FunctionalTests;

public class Helper(string currentRoute = "")
{
    public string CurrentRoute { get; set; } = currentRoute;

    public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content)!;
    }

    public string GetApiUrl() => $"http://localhost:5229/api/{CurrentRoute}";
}