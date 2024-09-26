using ChatProject.Context;
using ChatProject.Entities;
using ChatProject.Exceptions;
using ChatProject.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatDbContext Context;

    public MessageRepository(ChatDbContext context)
    {
        Context = context;
    }

    public async Task<List<Message>> GetAllMessages()
    {
        var messages = await Context.Messages.AsNoTracking()
            .Include(m => m.Content).ToListAsync();
        return messages;
    }

    public async Task<List<Message>> GetChatMessages(Guid chatId)
    {
        var messages = await Context.Messages
            .Include(m => m.Content)
            .Where(m => m.ChatId == chatId).ToListAsync();
        return messages;
    }

    public async Task<Message> GetMessageById(int id)
    {
        var message = await Context.Messages.SingleOrDefaultAsync(m => m.Id == id);
        if (message is null) throw new MessageNotFoundException();
        return message;
    }

    public async Task<Message> GetChatMessageById(Guid chatid, int messageId)
    {
        var message = await Context.Messages
            .SingleOrDefaultAsync(m => m.Id == messageId && m.ChatId == chatid);
        if (message is null) throw new MessageNotFoundException();
        return message;
    }

    public async Task AddMessage(Message message)
    {
        await Context.Messages.AddAsync(message);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateMessage(Message message)
    {
        Context.Messages.Update(message);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteMessage(Message message)
    {
        Context.Messages.Remove(message);
        await Context.SaveChangesAsync();
    }
}