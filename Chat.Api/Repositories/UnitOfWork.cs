using Chat.Api.Context;
using Chat.Api.Repositories.Abstract;

namespace Chat.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ChatDbContext _context;
    
    public UnitOfWork(ChatDbContext context) => _context = context;

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