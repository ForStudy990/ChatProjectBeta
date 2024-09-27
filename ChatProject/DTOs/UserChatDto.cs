namespace ChatProject.DTOs;

public class UserChatDto
{
    public Guid Id { get; set; }
    public Guid ReceiverId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
}