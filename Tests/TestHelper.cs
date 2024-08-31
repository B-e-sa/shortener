using Newtonsoft.Json;

namespace Shortener.Tests
{
    public class TestHelper(string currentRoute = "")
    {
        public string CurrentRoute { get; set; } = currentRoute;

        public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content)!;
        }

        public string GetApiUrl() => $"http://localhost:5229/api/{CurrentRoute}";
    }
}