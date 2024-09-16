using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Shortener.Domain.Common;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Shortener.Domain.Entities;

public class Url : BaseEntity
{
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public string Title { get; set; }
    public int Visits { get; set; }
    public int? UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}