using ChatProject.Entities;

namespace ChatProject.Repositories.Abstract;

public interface IUserChatRepository
{
    Task AddUserChat(UserChat userChat);
    Task DeleteUserChat(UserChat userChat);
    Task<UserChat> GetUserChat(Guid senderid, Guid chatId);
}