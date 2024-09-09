using System.Collections.Generic;
using Shortener.Domain.Common;

namespace Shortener.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Url> Urls { get; } = [];
}