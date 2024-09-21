using Shortener.Domain.Common;
using System.Text.Json.Serialization;

namespace Shortener.Domain.Entities;

public class Url : BaseEntity
{
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; } = CodeGenerator.Alphanumeric(4);
    public string Title { get; set; }
    public int Visits { get; set; }
    public int? UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}