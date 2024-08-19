using Newtonsoft.Json;

namespace Shortener.Tests
{
    public class TestHelper
    {
        public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content)!;
        }
    }
}