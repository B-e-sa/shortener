namespace Shortener.Application.Common.Models;

public class UserDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool ConfirmedEmail { get; set; }
}
