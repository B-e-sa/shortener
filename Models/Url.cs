namespace Shortener.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string Title { get; set; }
        public int Visits { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}