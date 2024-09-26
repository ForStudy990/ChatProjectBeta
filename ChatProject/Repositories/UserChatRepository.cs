using ChatProject.Context;
using ChatProject.Entities;
using ChatProject.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Repositories;

public class UserChatRepository : IUserChatRepository
{
    private readonly ChatDbContext Context;

    public UserChatRepository(ChatDbContext context)
    {
        Context = context;
    }

    public async Task AddUserChat(UserChat userChat)
    {
        await Context.AddAsync(userChat);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteUserChat(UserChat userChat)
    {
        Context.UserChats.Remove(userChat);
        await Context.SaveChangesAsync();
    }

    public async Task<UserChat> GetUserChat(Guid senderid, Guid chatId)
    {
        var userchat =
            await Context.UserChats
                .SingleOrDefaultAsync(uc => uc.SenderId == senderid && uc.ChatId == chatId);
        //make a custom exception
        if (userchat is null) throw new Exception("Not found UserChat");
        return userchat;
    }
}