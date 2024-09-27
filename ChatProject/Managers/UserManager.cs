using ChatProject.DTOs;
using ChatProject.Entities;
using ChatProject.Extensions;
using ChatProject.Repositories.Abstract;

namespace ChatProject.Managers;

public class UserManager
{
    private IUnitOfWork _unitOfWork;

    public UserManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllUsers();
        return users.ParseToDto();
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(id);
        return user.ParseToDto();
    }

    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(username);
        return user.ParseToDto();
    }
    
}