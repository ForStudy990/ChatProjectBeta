using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Models;
using Chat.Api.Repositories.Abstract;
using Chat.Api.Extensions;
using Chat.Api.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Managers;

public class UserManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtManager _jwtManager;
    private readonly UserHelper _userHelper;

    public UserManager(IUnitOfWork unitOfWork, JwtManager jwtManager, UserHelper userHelper)
    {
        _unitOfWork = unitOfWork;
        _jwtManager = jwtManager;
        _userHelper = userHelper;
    }

    public async Task<UserDto> UpdateBio(string bio)
    {
        var userid = _userHelper.GetUserId();
        var user = await _unitOfWork.UserRepository.GetUserById(userid);
        user.Bio = bio;
        await _unitOfWork.UserRepository.UpdateUser(user);
        return user.ParseToDto();
    }
    
    public async Task<UserDto> UpdateGeneralInfo(UpdateGeneralInfoModel model)
    {
        var userid = _userHelper.GetUserId();
        var user = await _unitOfWork.UserRepository.GetUserById(userid);

        var check = false;
        if (model.FirstName != null)
        {
            user.FirstName = model.FirstName;
            check = true;
        }

        if (model.LastName != null)
        {
            user.LastName = model.LastName;
            check = true;
        }

        if (model.Age != null)
        {
            user.Age = model.Age;
            check = true;
        }
        if (model.Gender != null)
        {
            user.Gender = model.Gender;
            check = true;
        }

        if (check)
            await _unitOfWork.UserRepository.UpdateUser(user);
        
        return user.ParseToDto();
    }

    public async Task<UserDto> UpdateUserName(string username)
    {
        var userid = _userHelper.GetUserId();
        var user = await _unitOfWork.UserRepository.GetUserById(userid);
        
        user.UserName = username;
        
        await _unitOfWork.UserRepository.UpdateUser(user);
        
        return user.ParseToDto();
    }
    
    public async Task<UserDto> UpdatePassword(string password)
    {
        var userid = _userHelper.GetUserId();
        var user = await _unitOfWork.UserRepository.GetUserById(userid);
        
        var passwordHash = new PasswordHasher<User>().HashPassword(user, password);
        if(passwordHash == user.PasswordHash)
            return user.ParseToDto();
        user.PasswordHash = passwordHash;
        await _unitOfWork.UserRepository.UpdateUser(user);
        
        return user.ParseToDto();
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