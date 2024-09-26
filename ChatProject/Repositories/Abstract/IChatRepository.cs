using ChatProject.Entities;

namespace ChatProject.Repositories.Abstract;

public interface IChatRepository
{
    Task<List<Chat>> GetAllChats();
    Task<List<Chat>> GetAllUserChats(Guid userId);
    Task<Chat> GetUserChatById(Guid userId, Guid chatId);
    Task<Tuple<bool, Chat?>> CheckCheckExist(Guid receiverId, Guid senderId);
    Task AddChat(Chat chat);
    Task UpdateChat(Chat chat);
    Task DeleteChat(Chat chat);
}