using ChatProject.DTOs;
using ChatProject.Entities;
using ChatProject.Extensions;
using ChatProject.Models;
using ChatProject.Repositories.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<string> Register(RegisterUserModel model)
    {
        
        var user = new User()
        {
            UserName = model.UserName,
            FirstName = model.FirstName,
        };
        var passwordhash = new PasswordHasher<User>().HashPassword(user, model.Password);
        user.PasswordHash = passwordhash;
        user.Id = Guid.NewGuid();
        await _unitOfWork.UserRepository.AddUser(user);
        return "Registred Successfully";
    }

    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(username);
        return user.ParseToDto();
    }
    
}