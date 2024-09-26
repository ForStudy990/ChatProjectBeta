using ChatProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserChat> UserChats { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Content> Contents { get; set; }
    
}