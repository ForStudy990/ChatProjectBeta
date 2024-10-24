using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Models;
using Chat.Api.Repositories.Abstract;
using Chat.Api.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Managers;

public class UserManager
{
    private IUnitOfWork _unitOfWork;
    private JwtManager _jwtManager;

    public UserManager(IUnitOfWork unitOfWork, JwtManager jwtManager)
    {
        _unitOfWork = unitOfWork;
        _jwtManager = jwtManager;
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

    public async Task<string> Login(LoginModel model)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(model.Username);
        if(user == null) throw new Exception("Invalid username");
        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if(result == PasswordVerificationResult.Failed) throw new Exception("Invalid password");

        var token = _jwtManager.GenerateToken(user);
        
        return token;
    }
    
}