using ChatProject.Context;
using ChatProject.Entities;
using ChatProject.Exceptions;
using ChatProject.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Repositories;

public class ChatRepository : IChatRepository
{
    
    private readonly ChatDbContext Context;

    public ChatRepository(ChatDbContext context)
    {
        Context = context;
    }

    public async Task<List<Chat>> GetAllChats()
    {
        var chats = await Context.Chats.AsNoTracking().ToListAsync();
        return chats;
    }

    public async Task<List<Chat>> GetAllUserChats(Guid userId)
    {
        var userChats = await Context.UserChats.Where(c => c.SenderId == userId)
            .ToListAsync();
        List<Chat> sortedChats = new();
            foreach (var chat in userChats)
            {
                var sortedChat = await Context.Chats.Include(ch => ch.Messages)
                    .ThenInclude(m => m.Content)
                    .SingleAsync(ch => ch.Id == chat.ChatId);
                sortedChats.Add(sortedChat);
            }

            return sortedChats;
    }

    public async Task<Chat> GetUserChatById(Guid userId, Guid chatId)
    {
        var userChat =
            await Context.UserChats
                .SingleOrDefaultAsync((uc => uc.SenderId == userId && uc.ChatId == chatId));
        if (userChat is null)
            throw new ChatNotFoundException();
        var chat = await Context.Chats.SingleAsync(c => c.Id == userChat.ChatId);
        return chat;
    }

    public async Task<Tuple<bool, Chat?>> CheckCheckExist(Guid receiverId, Guid senderId)
    {
        var userChat =
            await Context.UserChats.
                FirstOrDefaultAsync(uc => uc.SenderId == senderId && uc.ReceiverId == receiverId);
        if (userChat is not null)
        {
            var chat = await GetUserChatById(userChat.SenderId, userChat.ChatId);
            return new (true,chat);
        }

        return new(false, null);



    }

    public async Task AddChat(Chat chat)
    {
        await Context.Chats.AddAsync(chat);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChat(Chat chat)
    {
        Context.Chats.Update(chat);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteChat(Chat chat)
    {
        Context.Chats.Remove(chat);
        await Context.SaveChangesAsync();
    }
}