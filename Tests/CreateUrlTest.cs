using Xunit;

namespace Shortener.Tests
{
    public class MyHttpTests
    {
        [Fact]
        public async Task TestHttpRequest()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5229");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}