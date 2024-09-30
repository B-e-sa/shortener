using System.Collections.Generic;
using Shortener.Domain.Common;

namespace Shortener.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool ConfirmedEmail { get; set; }
    public EmailVerification? EmailVerification { get; set;  }
    public NewPasswordRequest? NewPasswordRequest { get; set; }
    public ICollection<Url> Urls { get; } = [];
}