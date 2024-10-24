using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public string? Gender { get; set; }
    public string? Bio { get; set; }
    public byte? Age { get; set; }
    public string? Role { get; set; }
    public List<UserChat>? UserChats { get; set; }
    public DateTime CreatedAt => DateTime.UtcNow;
    public byte[]? PhotoData { get; set; }



}