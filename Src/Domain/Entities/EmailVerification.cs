using Shortener.Domain.Common;
using System.Text.Json.Serialization;

namespace Shortener.Domain.Entities
{
    public class EmailVerification : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
