using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class UserChat
{
    public Guid Id { get; set; }
    public Guid ReceiverId { get; set; }
    [Required]
    public Guid SenderId { get; set; }
    public User? Sender { get; set; }
    public Guid ChatId { get; set; }
    public Chat? Chat { get; set; }
    
}