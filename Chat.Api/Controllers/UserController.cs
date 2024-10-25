using Chat.Api.DTOs;
using Chat.Api.Exceptions;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Entities;
using Chat.Api.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly string Key = "Users";
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
       var users = await _userManager.GetAllUsers();
        
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("get-user-by-id")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var user = await _userManager.GetUserById(userId);
            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    
    [Authorize]
    [HttpPut("update-user-bio")]
    public async Task<IActionResult> UpdateBio([FromBody] string bio)
    {
        var userDto = await _userManager.UpdateBio(bio);
        return Ok(userDto);
    }
   
    [Authorize]
    [HttpPut("update-user-general-info")]
    public async Task<IActionResult> UpdateGeneralInfo([FromBody] UpdateGeneralInfoModel model)
    {
        var userDto = await _userManager.UpdateGeneralInfo(model);
        return Ok(userDto);
    }
    [Authorize]
    [HttpPut("update-username")]
    public async Task<IActionResult> UpdateUserName([FromBody] string userName)
    {
        var userDto = await _userManager.UpdateUserName(userName);
        return Ok(userDto);
    }
    
    [Authorize]
    [HttpPut("update-user-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] string password)
    {
        var userDto = await _userManager.UpdatePassword(password);
        return Ok(userDto);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        var result = await _userManager.Register(model);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var result = await _userManager.Login(model);
            return Ok(result);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}