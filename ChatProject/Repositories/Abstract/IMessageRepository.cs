using ChatProject.Entities;

namespace ChatProject.Repositories.Abstract;

public interface IMessageRepository
{
    Task<List<Message>> GetAllMessages();
    Task<List<Message>> GetChatMessages(Guid chatId);
    Task<Message> GetMessageById(int id);
    Task<Message> GetChatMessageById(Guid chatid, int messageId);
    Task AddMessage(Message message);
    Task UpdateMessage(Message message);
    Task DeleteMessage(Message message);

}