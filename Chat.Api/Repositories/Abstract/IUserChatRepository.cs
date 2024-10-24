using Chat.Api.Entities;

namespace Chat.Api.Repositories.Abstract;

public interface IUserChatRepository
{
    Task AddUserChat(UserChat userChat);
    Task DeleteUserChat(UserChat userChat);
    Task<UserChat> GetUserChat(Guid senderid, Guid chatId);
}