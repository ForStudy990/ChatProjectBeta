using Chat.Api.Entities;

namespace Chat.Api.Repositories.Abstract;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(Guid id);
    Task<User?> GetUserByUserName(string username);

    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}