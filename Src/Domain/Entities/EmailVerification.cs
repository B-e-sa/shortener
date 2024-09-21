using Shortener.Domain.Common;
using System.Text.Json.Serialization;

namespace Shortener.Domain.Entities
{
    public class EmailVerification : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = CodeGenerator.Numbers(6);
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
