using ChatProject.Context;
using ChatProject.Entities;
using ChatProject.Exceptions;
using ChatProject.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ChatDbContext Context;

    public UserRepository(ChatDbContext context)
    {
        Context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await Context.Users.AsNoTracking().ToListAsync();
        return users;
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user is null) throw new UserNotFoundException();
        return user;
    }

    public async Task<User?> GetUserByUserName(string username)
    {
        var user = await Context.Users
            .SingleOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        return user;
    }

    public async Task AddUser(User user)
    {
        Context.Users.Add(user);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        Context.Users.Update(user);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        Context.Users.Remove(user);
        await Context.SaveChangesAsync();
    }
}