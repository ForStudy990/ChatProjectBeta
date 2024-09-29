using ChatProject.Context;
using ChatProject.Repositories.Abstract;

namespace ChatProject.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ChatDbContext _context;

    private IChatRepository? _chatRepository { get; }
    public IChatRepository ChatRepository => 
    _chatRepository ?? new ChatRepository(_context);
    private IUserRepository? _userRepository { get; }
    public IUserRepository UserRepository =>
        _userRepository ?? new UserRepository(_context);
    private IMessageRepository? _messageRepository { get; }
    public IMessageRepository MessageRepository =>
    _messageRepository ?? new MessageRepository(_context);
    
    private IUserChatRepository? _userChatRepository { get; }
    public IUserChatRepository UserChatRepository =>
    _userChatRepository ?? new UserChatRepository(_context);
}