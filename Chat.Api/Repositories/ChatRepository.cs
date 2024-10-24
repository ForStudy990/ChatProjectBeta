using Chat.Api.Context;
using Chat.Api.Exceptions;
using Chat.Api.Repositories.Abstract;
using Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories;

public class ChatRepository : IChatRepository
{
    
    private readonly ChatDbContext Context;

    public ChatRepository(ChatDbContext context)
    {
        Context = context;
    }

    public async Task<List<Entities.Chat>> GetAllChats()
    {
        var chats = await Context.Chats.AsNoTracking().ToListAsync();
        return chats;
    }

    public async Task<List<Entities.Chat>> GetAllUserChats(Guid userId)
    {
        var userChats = await Context.UserChats.Where(c => c.SenderId == userId)
            .ToListAsync();
        List<Entities.Chat> sortedChats = new();
            foreach (var chat in userChats)
            {
                var sortedChat = await Context.Chats.Include(ch => ch.Messages)
                    .ThenInclude(m => m.Content)
                    .SingleAsync(ch => ch.Id == chat.ChatId);
                sortedChats.Add(sortedChat);
            }

            return sortedChats;
    }

    public async Task<Entities.Chat> GetUserChatById(Guid userId, Guid chatId)
    {
        var userChat =
            await Context.UserChats
                .SingleOrDefaultAsync((uc => uc.SenderId == userId && uc.ChatId == chatId));
        if (userChat is null)
            throw new ChatNotFoundException();
        var chat = await Context.Chats.SingleAsync(c => c.Id == userChat.ChatId);
        return chat;
    }

    public async Task<Tuple<bool, Entities.Chat?>> CheckCheckExist(Guid receiverId, Guid senderId)
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

    public async Task AddChat(Entities.Chat chat)
    {
        await Context.Chats.AddAsync(chat);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateChat(Entities.Chat chat)
    {
        Context.Chats.Update(chat);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteChat(Entities.Chat chat)
    {
        Context.Chats.Remove(chat);
        await Context.SaveChangesAsync();
    }
    
}