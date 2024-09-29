namespace ChatProject.Repositories.Abstract;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IChatRepository ChatRepository { get; }
    IMessageRepository MessageRepository { get; }
    IUserChatRepository UserChatRepository { get; }
    
}