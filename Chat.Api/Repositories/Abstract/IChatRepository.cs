using Chat.Api.Entities;

namespace Chat.Api.Repositories.Abstract;

public interface IChatRepository
{
    Task<List<Entities.Chat>> GetAllChats();
    Task<List<Entities.Chat>> GetAllUserChats(Guid userId);
    Task<Entities.Chat> GetUserChatById(Guid userId, Guid chatId);
    Task<Tuple<bool, Entities.Chat?>> CheckCheckExist(Guid receiverId, Guid senderId);
    Task AddChat(Entities.Chat chat);
    Task UpdateChat(Entities.Chat chat);
    Task DeleteChat(Entities.Chat chat);
}